using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieParade : MonoBehaviour
{
    public Zombie_Dissolve[] zombies;
    public Transform playerTransform;

    private List<Zombie_Dissolve> zombielist = new List<Zombie_Dissolve>();

    private const float distance = 20f;

    private const int cnt = 50;

    private int zombieCount;

    private bool safe = false;

    private const float destroyCount = 5f;

    private const int startCount = 50;

    private const int clearCount = 35;

    private const int randomCountRange = 0;

    private const float wateForSecond = 5f;

    // Start is called before the first frame update
    void Start()
    {
        zombieCount = cnt;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Last && !GameManager.instance.IsGameover && zombielist.Count <= startCount)
        {
            Spawn();
        }

        if (zombieCount <= clearCount && !safe)
            StartCoroutine(Cor_ShowSafeText5Sec());

    }

    private void Spawn()
    {
        Vector3 spawnPosition =
        GetRandomPointOnNavMesh(playerTransform.position, distance);

        Zombie_Dissolve selectedItem = zombies[Random.Range(randomCountRange, zombies.Length)];
        Zombie_Dissolve zombie = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        zombielist.Add(zombie);

        zombie.onDeath += () => Destroy(zombie.gameObject, destroyCount);

        zombie.onDeath += () => zombieCount--;

    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center�� �߽����� �������� maxDistance�� �� �ȿ����� ������ ��ġ �ϳ��� ����
        // Random.insideUnitSphere�� �������� 1�� �� �ȿ����� ������ �� ���� ��ȯ�ϴ� ������Ƽ
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        // ����޽� ���ø��� ��� ������ �����ϴ� ����
        NavMeshHit hit;

        // maxDistance �ݰ� �ȿ���, randomPos�� ���� ����� ����޽� ���� �� ���� ã��
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        // ã�� �� ��ȯ
        return hit.position;
    }

    IEnumerator Cor_ShowSafeText5Sec()
    {
        UIManager.instance.ActiveSafeHouse(true);
        yield return new WaitForSeconds(wateForSecond);
        UIManager.instance.ActiveSafeHouse(false);
        safe = true;
    }

}
