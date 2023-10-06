using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CreateTrigger : MonoBehaviour
{
    public Zombie_Boss bossPrefab;
    public Zombie zombiePrefab;
    public ZombieData[] zombieDatas; // ����� ���� �¾� �����͵�
    public Transform[] spawnPoints; // ���� AI�� ��ȯ�� ��ġ��

    public Transform bossSpawn;

    private List<Zombie> zombies = new List<Zombie>(); // ������ ������� ��� ����Ʈ

    public System.Action action;
    private int killPoint = 0;

    private bool isCreate = true;

    private const float destroyCount = 10f;

    private const int CreatedCount = 10;

    private const int addCount = 1;

    private const float waitForSecond = 5f;

    // Start is called before the first frame update
    void Start()
    {
        action += AddKillPoint;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isCreate && GameManager.instance.ZombieCount >= CreatedCount)
        {
            //CreateZombie();
           StartCoroutine(cor_ShowBossUI());
           CreateBoss();
        }
    }

    private void CreateZombie()
    {
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];

        // ���� ��ġ ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // �̸� ����� �ξ��� ���� ���������κ��� ���� ����
        Zombie zombie;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            zombie = Instantiate(zombiePrefab, spawnPoints[i].position, spawnPoints[i].rotation);

            // ������ ������ �ɷ�ġ �߰�
            zombie.Setup(zombieData);

            // ������ ������ ���� Ȯ���ϱ� ���� ����Ʈ�� ���
            zombies.Add(zombie);
            // ������ onDeath �̺�Ʈ�� �͸� �޼��带 ����ϰ� ����� ���� ����Ʈ���� ����
            zombie.onDeath += () => zombies.Remove(zombie); // ���ٽ� �̿� (�Է�) => ����
            // ����� ����� 5�� �ڿ� �ı�
            zombie.onDeath += () => Destroy(zombie.gameObject);

            // ��� �� ���� ���� �� ++
            zombie.onDeath += () => GameManager.instance.AddZombieCount(addCount);

            zombie.onDeath += () => action();
        }


    }

    void CreateBoss()
    {
        Zombie_Boss boss = Instantiate(bossPrefab, bossSpawn.position, bossSpawn.rotation);

        boss.onDeath += () => Destroy(boss.gameObject, destroyCount);

        isCreate = false;

    }

    void AddKillPoint()
    {
        killPoint++;

        AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.kill1,
            kill: killPoint);

        AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.kill10,
            kill: killPoint);
    }

    IEnumerator cor_ShowBossUI()
    {
        UIManager.instance.BossUI(true);
        yield return new WaitForSeconds(waitForSecond);
        UIManager.instance.BossUI(false);
    }
}
