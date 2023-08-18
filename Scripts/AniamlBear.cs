using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AniamlBear : LivingEntity
{
    public LayerMask whatIsTarget;
    private LivingEntity targetEntity; // ���� ���

    private NavMeshAgent navMeshAgent;

    private Animator AnimalAnimator;

    private AudioSource bearAudio;
    public AudioClip dieSound;
    public AudioClip angrySound;

    public GameObject itemPrefab;

    public System.Action onDie;

    private float speed = 1f;

    public float damage = 25f;
    public float timeBetAttack = 1f;
    private float lastAttackTime;

    private bool half = true;

    public bool hasTarget
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

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        AnimalAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bearAudio = GetComponent<AudioSource>();
        navMeshAgent.speed = speed;
        health = 200f;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        //if(!dead)
        //{
        //    Vector3 v = pos;
        //
        //    v.x += delta * Mathf.Sin(Time.time * speed);
        //
        //    // �¿� �̵��� �ִ�ġ �� ���� ó���� �̷��� ���ٿ� ���ְ� �ϳ׿�.
        //
        //    transform.position = v;
        //    AnimalAnimator.SetFloat("Move", 0.5f);
        //}
        //else
        //    AnimalAnimator.SetFloat("Move", 0f);

    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        //    Debug.Log("sdfsd");
    }
    //
    public override void Die()
    {
        base.Die();
        bearAudio.PlayOneShot(dieSound);

        DropItem();
        Collider[] colliders = GetComponents<Collider>();

        for (int i = 0; i < colliders.Length; i++)
            colliders[i].enabled = false;

        //AnimalAnimator.SetBool("Dead", true);

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        AnimalAnimator.SetTrigger("Die");
        this.onDie();

    }

    public virtual IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if (hasTarget)
            {
                //float movePower = move[Random.Range(0, move.Length)];
                AnimalAnimator.SetFloat("Move", 0.5f/*Random.Range(3, movePower)*/);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);

                if(health <= 50 && half)
                {
                    bearAudio.PlayOneShot(angrySound);
                    AnimalAnimator.SetFloat("Move", 1f);
                    navMeshAgent.speed = 2.5f;
                    half = false;
                }
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, whatIsTarget);
                navMeshAgent.isStopped = true;
                AnimalAnimator.SetFloat("Move", 0f);

                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity live = colliders[i].GetComponent<LivingEntity>();
                    if (live != null && !live.dead)
                    {
                        targetEntity = live;
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);
        }

    }
    public virtual void DropItem()
    {
        var go = Instantiate<GameObject>(this.itemPrefab);
        go.transform.position = this.gameObject.transform.position;
        go.SetActive(false);

        this.onDie = () =>
        {
            go.SetActive(true);
            //go.GetComponent<FiledItems>().GetItem();
            go.GetComponent<FiledItems>().SetItem(go.GetComponent<FiledItems>().GetItem());
        };

    }

    private void OnTriggerStay(Collider other)
    {
        if (!dead)
        {
            AnimalAnimator.SetTrigger("Attack");
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

}
