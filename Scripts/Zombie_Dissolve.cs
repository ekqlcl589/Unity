using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, ������̼� �ý��� ���� �ڵ� ��������

// ���� AI ����
public class Zombie_Dissolve : LivingEntity
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material mtrlOrg;
    [SerializeField] private Material mtrlDissolve;
    [SerializeField] private float dissolveTime = 2f;

    [SerializeField] private LayerMask whatIsTarget; // ���� ��� ���̾�

    private LivingEntity targetEntity; // ���� ���
    private NavMeshAgent navMeshAgent; // ��� ��� AI ������Ʈ

    [SerializeField] private ParticleSystem hitEffect; // �ǰ� �� ����� ��ƼŬ ȿ��
    [SerializeField] private ParticleSystem sunHitEffect;
    [SerializeField] private AudioClip deathSound; // ��� �� ����� �Ҹ�
    [SerializeField] private AudioClip hitSound; // �ǰ� �� ����� �Ҹ�
    [SerializeField] private AudioClip sunHitSound;

    private Animator zombieAnimator; // �ִϸ����� ������Ʈ
    private AudioSource zombieAudioPlayer; // ����� �ҽ� ������Ʈ
    private Renderer zombieRenderer; // ������ ������Ʈ

    [SerializeField] private GameObject itemPrefab;
    public System.Action onDie;

    private float damage; // ���ݷ�
    private const float timeBetAttack = 0.5f; // ���� ����
    private float lastAttackTime; // ������ ���� ����

    private bool sunHit = false;

    private const int sunDamage = 20;
    private const int dayDieCount = 1;

    private const float ColligionRange = 10f;


    // ������ ����� �����ϴ��� �˷��ִ� ������Ƽ
    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if (targetEntity != null && !targetEntity.Dead)
            {
                zombieAnimator.SetBool("Att", true);
                return true;
            }

            // �׷��� �ʴٸ� false
            zombieAnimator.SetBool("Att", false);
            return false;
        }
    }

    private void Awake()
    {
        // �ʱ�ȭ
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieAudioPlayer = GetComponent<AudioSource>();

        zombieRenderer = GetComponentInChildren<Renderer>();
    }

    // ���� AI�� �ʱ� ������ �����ϴ� �¾� �޼���
    public void Setup(ZombieData zombieData)
    {
        startingHealth = zombieData.Health;
        health = zombieData.Health;

        damage = zombieData.Damage;

        navMeshAgent.speed = zombieData.Speed;

        zombieRenderer.material.color = zombieData.SkinColor;
    }

    private void Start()
    {
        // ���� ������Ʈ Ȱ��ȭ�� ���ÿ� AI�� ���� ��ƾ ����
        StartCoroutine(UpdatePath());
        StartCoroutine(DayDie());
        //this.onDie += AddKillPoint;

    }

    private void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        zombieAnimator.SetBool("HasTarget", hasTarget);
        //Onday(); // �̷��� �ϸ� �� �Ǹ� �����°� Ȯ�� �ڷ�ƾ���� �غ���
    }

    // �ֱ������� ������ ����� ��ġ�� ã�� ��� ����
    private IEnumerator UpdatePath()
    {
        // ��� �ִ� ���� ���� ����
        while (!Dead)
        {
            if (hasTarget)
            {
                // �߰� ����� �����ϸ� ��θ� �����ϰ� ai �̵��� ��� ����
                zombieAnimator.SetFloat("Move", naveMeshSlowSpeed);

                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);

            }
            else
            {
                navMeshAgent.isStopped = true;
                zombieAnimator.SetFloat("Move", naveMeshStopSpeed);

                //10������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
                //��, whatisTarget ���̾ ���� �ݶ��̴��� ���������� ���͸� ����

                Collider[] colliders = Physics.OverlapSphere(transform.position, ColligionRange, whatIsTarget);
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
            // 0.25�� �ֱ�� ó�� �ݺ�
            yield return new WaitForSeconds(0.25f);
        }
    }

    // �������� �Ծ��� �� ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!Dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            zombieAudioPlayer.PlayOneShot(hitSound);
        }
        // LivingEntity�� OnDamage()�� �����Ͽ� ������ ����
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die()�� �����Ͽ� �⺻ ��� ó�� ����
        base.Die();

        _renderer.material = mtrlDissolve;
        DoDissolve(1, 0, dissolveTime);

        DropItem();
        zombieAnimator.SetBool("Att", false);
        //zombieAnimator.SetBool("Attack", false);

        // �ٸ� ai�� �������� �ʵ��� �ڽ��� ��� �ݶ��̴� ��Ȱ��ȭ 
        Collider[] zombieColls = GetComponents<Collider>();

        for (int i = 0; i < zombieColls.Length; i++)
        {
            zombieColls[i].enabled = false;
        }

        // ai ������ �����ϰ� �׺�޽� ������Ʈ ��Ȱ��ȭ 
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        zombieAnimator.SetTrigger("Die");

        zombieAudioPlayer.PlayOneShot(deathSound);

        this.onDie();
    }

    private void OnTriggerStay(Collider other)
    {
        // �ڽ��� ������� �ʾҰ�, �ֱ� ���� �������� timebetattack �̻� �ð��� �����ٸ� ����
        if (!Dead && Time.time >= lastAttackTime + timeBetAttack)
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
        // Ʈ���� �浹�� ���� ���� ������Ʈ�� ���� ����̶�� ���� ����
    }

    public void DropItem()
    {
        var go = Instantiate<GameObject>(this.itemPrefab);
        go.transform.position = this.gameObject.transform.position;
        go.SetActive(false);

        this.onDie = () =>
        {
            //AddKillPoint();
            go.SetActive(true);
            //go.GetComponent<FiledItems>().GetItem();
            go.GetComponent<FiledItems>().SetItem(go.GetComponent<FiledItems>().GetItem());
        };
    }

    public void Onday()
    {
        if (!Dead && !GameManager.instance.IsNight)
        {
            health -= sunDamage;

            if (health <= dieHealth && !Dead)
            {
                Die();
            }

        }
    }

    //�µ��� �ڷ�ƾ����

    private IEnumerator DayDie()
    {
        while (!Dead)
        {
            if (!GameManager.instance.IsNight && GameManager.instance.Last)
            {
                zombieAudioPlayer.PlayOneShot(sunHitSound);
                sunHit = true;
                sunHitEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                sunHitEffect.Play();

                health -= sunDamage;
                //zombieAudioPlayer.PlayOneShot(hitSound);

                if (health <= dieHealth && !Dead)
                {
                    AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.sunKill,
                         sun: dayDieCount);

                    Die();
                }

            }

            yield return new WaitForSeconds(dayDieCount);
        }
    }

    void DoDissolve(float start, float dest, float time) // �Ź� �� ����, ���� �� ������ ȣ��Ǵ� �ݹ��Լ� ����
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", start, "to", dest, "time", time,
            "onupdatetarget", gameObject,
            "onupdate", "TweenOnUpdate",
            "oncomplte", "TweenOnComplte",
            "easetype", iTween.EaseType.easeInOutCubic));
    }

    void TweenOnUpdate(float value)
    {
        _renderer.material.SetFloat("_SpllitValue", value);
    }

    void TweenOnComplte()
    {
        _renderer.material = mtrlOrg;
    }

}