using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Boss : LivingEntity
{
    [SerializeField] private LayerMask whatIsTarget; // ���� ��� ���̾�

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private ParticleSystem hitEffect; // �ǰ� �� ����� ��ƼŬ ȿ��

    [SerializeField] private AudioClip deathSound; // ��� �� ����� �Ҹ�
    [SerializeField] private AudioClip hitSound; // �ǰ� �� ����� �Ҹ�
    [SerializeField] private AudioClip angrySound; // ü���� ���� ��ġ ���Ϸ� ������ �� ����� ���� 

    private LivingEntity targetEntity; // ���� ���
    private NavMeshAgent navMeshAgent; // ��� ��� AI ������Ʈ

    private Animator zombieAnimator;

    private AudioSource zombieAudioPlayer;

    private const float damage = 30f;
    private const float timeBetAttack = 1f;
    private float lastAttackTime;

    private const float bossHalfHealth = 500f;

    private const float bossphase2Speed = 4.5f;

    private const float bossColligionRange = 10f;

    private const float paradeWaitTime = 5f;

    private bool angry = false;

    public System.Action onDie;

    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if (targetEntity != null && !targetEntity.Dead)
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

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.IsGameover)
        {
            return;
        }

    }

    private IEnumerator UpdatePath()
    {
        while (!Dead)
        {
            if (hasTarget)
            {
                // �߰� ����� �����ϸ� ��θ� �����ϰ� ai �̵��� ��� ����
                zombieAnimator.SetFloat("Move", naveMeshSlowSpeed);

                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);

                if (health <= bossHalfHealth)
                {
                    zombieAnimator.SetFloat("Move", naveMeshDefaultSpeed);
                    navMeshAgent.speed = bossphase2Speed;

                    if (!angry)
                    {
                        zombieAudioPlayer.PlayOneShot(angrySound);
                        angry = true;
                    }
                }

            }
            else
            {
                navMeshAgent.isStopped = true;
                zombieAnimator.SetFloat("Move", naveMeshStopSpeed);

                Collider[] colliders = Physics.OverlapSphere(transform.position, bossColligionRange, whatIsTarget);
                // ��� �ݶ��̴��� ��ȸ�ϸ鼭 ��� �ִ� LiveingEntiry ã��
                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity live = colliders[i].GetComponent<LivingEntity>();

                    // ������Ʈ�� �����ϰ� �ش� ������Ʈ�� ��� �ִٸ�
                    if (live != null && !live.Dead)
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
        if (!Dead)
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
        if (!Dead)
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

    IEnumerator cor_ShowZombieParade()
    {
        UIManager.instance.ZombieParade(true);
        yield return new WaitForSeconds(paradeWaitTime);
        UIManager.instance.ZombieParade(false);
        GameManager.instance.LastCheckData();

    }
}
