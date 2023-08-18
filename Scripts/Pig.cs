using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pig : LivingEntity
{
    public LayerMask whatIsTarget;
    private LivingEntity targetEntity; // ���� ���

    private Animator pigAnimator;
    private NavMeshAgent navMeshAgent;

    public GameObject itemPrefab;

    public System.Action onDie;

    public float speed = 1f;
    public float waitTime = 3f;

    protected bool hasTarget
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

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        pigAnimator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.speed = speed;
        health = 20f;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        //navMeshAgent.isStopped = false;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        Debug.Log("sdfsd"+1234);
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
        while (!dead)
        {
            if (hasTarget)
            {
                //float movePower = move[Random.Range(0, move.Length)];
                pigAnimator.SetFloat("Move", 0.5f/*Random.Range(3, movePower)*/);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);
                navMeshAgent.isStopped = true;
                pigAnimator.SetFloat("Move", 0f);

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
    public void DropItem()
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

}
