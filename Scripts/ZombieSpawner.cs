﻿using System.Collections.Generic;
using UnityEngine;

// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Zombie_Dissolve zombiePrefab; // 생성할 좀비 원본 프리팹

    [SerializeField] private ZombieData[] zombieDatas; // 사용할 좀비 셋업 데이터들
    [SerializeField] private Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

    private List<Zombie_Dissolve> zombies = new List<Zombie_Dissolve>(); // 생성된 좀비들을 담는 리스트

    private const int spawnCount = 2;

    private const float destroyCount = 5f;

    private const int createCount = 12;

    private const int zeroRange = 0;

    public System.Action action;
    private void Start()
    {
        action += AddKillPoint;

    }
    private void Update()
    {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        // 좀비를 모두 물리친 경우 다음 스폰 실행
        if (zombies.Count <= spawnCount && GameManager.instance.IsNight)
        {
            SpawnWave();
        }

        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI()
    {
        // 현재 웨이브와 남은 적 수 표시
        // UIManager.instance.UpdateWaveText(wave, zombies.Count);
    }

    // 현재 웨이브에 맞춰 좀비들을 생성
    private void SpawnWave()
    {

        //spawnCount 만큼 좀비 생성
        for (int i = 0; i < createCount; i++)
            CreateZombie();
    }

    // 좀비를 생성하고 생성한 좀비에게 추적할 대상을 할당
    private void CreateZombie()
    {
        // 사용할 좀비 데이터 랜덤으로 결정
        ZombieData zombieData = zombieDatas[Random.Range(zeroRange, zombieDatas.Length)];

        // 생성 위치 랜덤
        Transform spawnPoint = spawnPoints[Random.Range(zeroRange, spawnPoints.Length)];

        // 미리 만들어 두었던 좀비 프리팹으로부터 좀비 생성

        Zombie_Dissolve zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        // 생성한 좀비의 능력치 추가
        zombie.Setup(zombieData);

        // 생성된 좀비의 수를 확인하기 위해 리스트에 등록
        zombies.Add(zombie);

        // 좀비의 onDeath 이벤트에 익명 메서드를 등록하고 사망한 좀비를 리스트에서 제거
        zombie.onDeath += () => zombies.Remove(zombie); // 람다식 이용 (입력) => 내용
        // 사망한 좀비는 5초 뒤에 파괴
        zombie.onDeath += () => Destroy(zombie.gameObject, destroyCount);

        zombie.onDeath += () => AddKillPoint();
    }

    void AddKillPoint()
    {
        AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.kill1,
            kill: GameManager.instance.ZombieCount);

        AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.kill10,
            kill: GameManager.instance.ZombieCount);
    }
}