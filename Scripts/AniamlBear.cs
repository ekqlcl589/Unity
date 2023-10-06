using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AniamlBear : LivingEntity
{
    [SerializeField] private LayerMask whatIsTarget;
    private LivingEntity targetEntity; // 추적 대상

    private NavMeshAgent navMeshAgent;

    private Animator AnimalAnimator;

    private AudioSource bearAudio;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip angrySound;

    [SerializeField] private GameObject itemPrefab;

    public System.Action onDie;

    private float speed = 1f;

    private float damage = 25f;
    private float timeBetAttack = 1f;
    private float lastAttackTime;

    private bool half = true;

    private const float bearHp = 200f;
    private const float bearPhase2Hp = 50f;
    private const float bearPhase2Speed = 2.5f;

    public bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null && !targetEntity.Dead)
            {
                return true;
            }

            // 그렇지 않다면 false
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
        health = bearHp;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();
        bearAudio.PlayOneShot(dieSound);

        DropItem();
        Collider[] colliders = GetComponents<Collider>();

        for (int i = 0; i < colliders.Length; i++)
            colliders[i].enabled = false;

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        AnimalAnimator.SetTrigger("Die");
        this.onDie();

    }

    public virtual IEnumerator UpdatePath()
    {
        while (!Dead)
        {
            if (hasTarget)
            {
                AnimalAnimator.SetFloat("Move", naveMeshSlowSpeed);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);

                if (health <= bearPhase2Hp && half)
                {
                    bearAudio.PlayOneShot(angrySound);
                    AnimalAnimator.SetFloat("Move", naveMeshDefaultSpeed);
                    navMeshAgent.speed = bearPhase2Speed;
                    half = false;
                }
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, navMeshRange, whatIsTarget);
                navMeshAgent.isStopped = true;
                AnimalAnimator.SetFloat("Move", naveMeshStopSpeed);

                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity live = colliders[i].GetComponent<LivingEntity>();
                    if (live != null && !live.Dead)
                    {
                        targetEntity = live;
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(waitForSecond);
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
            go.GetComponent<FiledItems>().SetItem(go.GetComponent<FiledItems>().GetItem());
        };

    }

    private void OnTriggerStay(Collider other)
    {
        if (!Dead)
        {
            AnimalAnimator.SetTrigger("Attack");
            if (Time.time >= lastAttackTime + timeBetAttack)
            {
                LivingEntity attackTarget = other.GetComponent<LivingEntity>();

                if (attackTarget != null && attackTarget == targetEntity)
                {
                    lastAttackTime = Time.time;
                    // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산 
                    Vector3 hitPoint = other.ClosestPoint(transform.position);
                    Vector3 hitNormal = transform.position - other.transform.position;
                    // 공격
                    attackTarget.OnDamage(damage, hitPoint, hitNormal);
                }

            }

        }

    }

}
