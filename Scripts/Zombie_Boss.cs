using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Boss : LivingEntity
{
    [SerializeField] private LayerMask whatIsTarget; // 추적 대상 레이어

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private ParticleSystem hitEffect; // 피격 시 재생할 파티클 효과

    [SerializeField] private AudioClip deathSound; // 사망 시 재생할 소리
    [SerializeField] private AudioClip hitSound; // 피격 시 재생할 소리
    [SerializeField] private AudioClip angrySound; // 체력이 일정 수치 이하로 내려갈 시 재생할 사운드 

    private LivingEntity targetEntity; // 추적 대상
    private NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트

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
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null && !targetEntity.Dead)
            {
                return true;
            }

            // 그렇지 않다면 false
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
                // 추격 대상이 존재하면 경로를 갱신하고 ai 이동을 계속 진행
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
                // 모든 콜라이더를 순회하면서 살아 있는 LiveingEntiry 찾기
                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity live = colliders[i].GetComponent<LivingEntity>();

                    // 컴포넌트가 존재하고 해당 컴포넌트가 살아 있다면
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
                    // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산 
                    Vector3 hitPoint = other.ClosestPoint(transform.position);
                    Vector3 hitNormal = transform.position - other.transform.position;
                    // 공격
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
