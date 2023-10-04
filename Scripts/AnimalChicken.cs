using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalChicken : LivingEntity
{
    public LayerMask whatIsTarget;
    private LivingEntity targetEntity; // 추적 대상

    private NavMeshAgent navMeshAgent;

    private Animator AnimalAnimator;

    private AudioSource chickenAudio;
    public AudioClip DieSound;

    public GameObject itemPrefab;

    public System.Action onDie;

    private float speed = 1f;

    private const chickenNavMeshRange = 10f;

    public bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null && !targetEntity.dead)
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
        chickenAudio = GetComponent<AudioSource>();
        navMeshAgent.speed = speed;
        health = startAnimalHealth;
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
        chickenAudio.PlayOneShot(DieSound);

        AnimalAnimator.SetFloat("Move", 0f);
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
                AnimalAnimator.SetFloat("Move", naveMeshSlowSpeed);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            { // 타겟이 없으면 Move 값 0 == Idle 상태로 대기 
                Collider[] colliders = Physics.OverlapSphere(transform.position, chickenNavMeshRange, whatIsTarget);
                navMeshAgent.isStopped = true;
                AnimalAnimator.SetFloat("Move", naveMeshStopSpeed);

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

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Grass")
        {
            AnimalAnimator.SetFloat("Move", naveMeshStopSpeed);
            AnimalAnimator.SetTrigger("Eat");
        }
    }
}
