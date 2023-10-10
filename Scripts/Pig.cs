using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pig : LivingEntity
{
    public LayerMask whatIsTarget;
    private LivingEntity targetEntity; // 추적 대상

    private Animator pigAnimator;
    private NavMeshAgent navMeshAgent;

    public GameObject itemPrefab;

    public System.Action onDie;

    private float speed = 1f;

    protected bool hasTarget
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

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        pigAnimator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.speed = speed;
        health = startAnimalHealth;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.IsGameover)
        {
            return;
        }

        //navMeshAgent.isStopped = false;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die()
    {
        base.Die();

        //AnimalAnimator.SetFloat("Move", 0f);
        DropItem();
        Collider[] colliders = GetComponents<Collider>();

        for (int i = 0; i < colliders.Length; i++)
            colliders[i].enabled = false;

        //AnimalAnimator.SetBool("Dead", true);

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        pigAnimator.SetBool("Dead", true);

        this.onDie();
    }

    public IEnumerator UpdatePath()
    {
        while (!Dead)
        {
            if (hasTarget)
            {
                //float movePower = move[Random.Range(0, move.Length)];
                pigAnimator.SetFloat("Move", naveMeshSlowSpeed);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, navMeshRange, whatIsTarget);
                navMeshAgent.isStopped = true;
                pigAnimator.SetFloat("Move", naveMeshStopSpeed);

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
    public void DropItem()
    {
        var go = Instantiate<GameObject>(this.itemPrefab);
        go.transform.position = this.gameObject.transform.position;
        go.SetActive(false);

        this.onDie = () =>
        {
            go.SetActive(true);
            //go.GetComponent<FiledItems>().GetItem();
            go.GetComponent<FiledItems>().SetItemDataSwap(go.GetComponent<FiledItems>().GetItem());
        };

    }

}
