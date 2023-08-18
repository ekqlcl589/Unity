using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Boss : LivingEntity
{
    public LayerMask whatIsTarget; // ���� ��� ���̾�

    private LivingEntity targetEntity; // ���� ���
    private NavMeshAgent navMeshAgent; // ��� ��� AI ������Ʈ

    private Animator zombieAnimator;

    public GameObject itemPrefab;

    public System.Action onDie;

    public ParticleSystem hitEffect; // �ǰ� �� ����� ��ƼŬ ȿ��

    public AudioClip deathSound; // ��� �� ����� �Ҹ�
    public AudioClip hitSound; // �ǰ� �� ����� �Ҹ�
    public AudioClip angrySound; // ü���� ���� ��ġ ���Ϸ� ������ �� ����� ���� 
    private AudioSource zombieAudioPlayer;

    public float damage = 30f;
    public float timeBetAttack = 1f;
    private float lastAttackTime;

    private bool angry = false;

    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // �׷��� �ʴٸ� false
            return false;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieAudioPlayer = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePath());
        //StartCoroutine(cor_ShowZombieParade());

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

    }

    private IEnumerator UpdatePath()
    {
        while(!dead)
        {
            if (hasTarget)
            {
                // �߰� ����� �����ϸ� ��θ� �����ϰ� ai �̵��� ��� ����
                zombieAnimator.SetFloat("Move", 0.5f);

                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);

                if(health <= 500)
                {
                    zombieAnimator.SetFloat("Move", 1f);
                    navMeshAgent.speed = 4.5f;

                    if(!angry)
                    {
                        zombieAudioPlayer.PlayOneShot(angrySound);
                        angry = true;
                    }
                }

            }
            else
            {
                navMeshAgent.isStopped = true;
                zombieAnimator.SetFloat("Move", 0f);

                Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, whatIsTarget);
                // ��� �ݶ��̴��� ��ȸ�ϸ鼭 ��� �ִ� LiveingEntiry ã��
                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity live = colliders[i].GetComponent<LivingEntity>();

                    // ������Ʈ�� �����ϰ� �ش� ������Ʈ�� ��� �ִٸ�
                    if (live != null && !live.dead)
                    {
                        targetEntity = live;
                        break;
                    }
                }

            }

            yield return new WaitForSeconds(1f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            zombieAudioPlayer.PlayOneShot(hitSound);
        }

        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        Collider[] zombieColls = GetComponents<Collider>();

        for (int i = 0; i < zombieColls.Length; i++)
        {
            zombieColls[i].enabled = false;
        }

        zombieAnimator.SetTrigger("Death");

        zombieAudioPlayer.PlayOneShot(deathSound);

        StartCoroutine(cor_ShowZombieParade());
        //this.onDie();

    }

    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (!dead)
        {
            zombieAnimator.SetTrigger("Attack");
            if (Time.time >= lastAttackTime + timeBetAttack)
            {
                LivingEntity attackTarget = other.GetComponent<LivingEntity>();

                if (attackTarget != null && attackTarget == targetEntity)
                {
                    lastAttackTime = Time.time;
                    // ������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ��� 
                    Vector3 hitPoint = other.ClosestPoint(transform.position);
                    Vector3 hitNormal = transform.position - other.transform.position;
                    // ����
                    attackTarget.OnDamage(damage, hitPoint, hitNormal);
                }

            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
    }

    IEnumerator cor_ShowZombieParade()
    {
        UIManager.instance.ZombieParade(true);
        yield return new WaitForSeconds(5);
        UIManager.instance.ZombieParade(false);
        GameManager.instance.last = false;

    }
}
