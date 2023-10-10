using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public Transform playerTransform;

    private List<Item> itemList = new List<Item>();

    private const float maxDistance = 50f;

    private const float timeBetSpawnMax = 7f; // 최대 시간 간격
    private const float timeBetSpawnMin = 2f; // 최소 시간 간격
    private const float timeBetSpawn; // 생성 간격

    private float lastSpawnTime; // 마지막 생성 시점

    private const int init = 0;

    private const float destroyCount = 5f;

    // Start is called before the first frame update
    void Start()
    {
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = init;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null)
        {
            // 마지막 생성 시간 갱신
            lastSpawnTime = Time.time;
            // 생성 주기를 랜덤으로 변경
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            // 아이템 생성 실행
            Spawn();
        }

    }

    private void Spawn()
    {
        // 플레이어 근처에서 내비메시 위의 랜덤 위치 가져오기
        Vector3 spawnPosition =
            GetRandomPointOnNavMesh(playerTransform.position, maxDistance);
        // 바닥에서 0.86만큼 위로 올리기
        //spawnPosition += Vector3.up * 0.86f;

        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
        GameObject selectedItem = items[Random.Range(init, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        Destroy(item, destroyCount);

    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center를 중심으로 반지름이 maxDistance인 구 안에서의 랜덤한 위치 하나를 저장
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        // 내비메시 샘플링의 결과 정보를 저장하는 변수
        NavMeshHit hit;

        // maxDistance 반경 안에서, randomPos에 가장 가까운 내비메시 위의 한 점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        // 찾은 점 반환
        return hit.position;
    }
}
