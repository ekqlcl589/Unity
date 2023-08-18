using System.Collections;
using System.Collections.Generic;
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
        if(other.CompareTag("Player") && isCreate && GameManager.instance.zombieCount >= 10)// && GameManager.instance.isNight)
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
            zombie.onDeath += () => GameManager.instance.AddZombieCount(1);

            zombie.onDeath += () => action();
        }


    }

    void CreateBoss()
    {
        Zombie_Boss boss = Instantiate(bossPrefab, bossSpawn.position, bossSpawn.rotation);

        boss.onDeath += () => Destroy(boss.gameObject, 10f);

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
        yield return new WaitForSeconds(5f);
        UIManager.instance.BossUI(false);
    }
}
