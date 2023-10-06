using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CreateTrigger : MonoBehaviour
{
    public Zombie_Boss bossPrefab;
    public Zombie zombiePrefab;
    public ZombieData[] zombieDatas; // 사용할 좀비 셋업 데이터들
    public Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

    public Transform bossSpawn;

    private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비들을 담는 리스트

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

        // 생성 위치 랜덤
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // 미리 만들어 두었던 좀비 프리팹으로부터 좀비 생성
        Zombie zombie;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            zombie = Instantiate(zombiePrefab, spawnPoints[i].position, spawnPoints[i].rotation);

            // 생성한 좀비의 능력치 추가
            zombie.Setup(zombieData);

            // 생성된 좀비의 수를 확인하기 위해 리스트에 등록
            zombies.Add(zombie);
            // 좀비의 onDeath 이벤트에 익명 메서드를 등록하고 사망한 좀비를 리스트에서 제거
            zombie.onDeath += () => zombies.Remove(zombie); // 람다식 이용 (입력) => 내용
            // 사망한 좀비는 5초 뒤에 파괴
            zombie.onDeath += () => Destroy(zombie.gameObject);

            // 사망 시 잡은 좀비 수 ++
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
