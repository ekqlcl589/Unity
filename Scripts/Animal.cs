using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : LivingEntity
{
    public LayerMask whatIsTarget;
    private LivingEntity targetEntity; // ���� ���

    private NavMeshAgent navMeshAgent;

    private Animator AnimalAnimator;

    public GameObject itemPrefab; // serialize -> ������ ������ �ٽ� Ȯ��

    public System.Action onDie;

    private const float findColliderRange = 5f;
    private float speed = 1f; // ������ ������ Ȯ��

    private AudioSource pigAudio;
    public AudioClip dieSound;


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
        pigAudio = GetComponent<AudioSource>();
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

        this.onDeath += () => Destroy(this);

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
        pigAudio.PlayOneShot(dieSound);

        AnimalAnimator.SetFloat("Move", naveMeshStopSpeed);
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
        while(!dead)
        {
            if(hasTarget)
            {
                AnimalAnimator.SetFloat("Move", naveMeshSlowSpeed);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, findColliderRange, whatIsTarget);
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
}
