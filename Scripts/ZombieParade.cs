using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieParade : MonoBehaviour
{
    [SerializeField] private Zombie_Dissolve[] zombies;
    [SerializeField] private Transform playerTransform;

    private List<Zombie_Dissolve> zombielist = new List<Zombie_Dissolve>();

    private float distance = 20f;

    private int cnt = 50;

    private bool safe = false;

    private const float destroyCount = 5f;

    private const int startCount = 50;

    private const int clearCount = 35;

    private const int randomCountRange = 0;

    private const float wateForSecond = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Last && !GameManager.instance.isGameover && zombielist.Count <= startCount)
        {
            Spawn();
        }

        if (cnt <= clearCount && !safe)
        {
            GameManager.instance.Clear = true;
            StartCoroutine(Cor_ShowSafeText5Sec());
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition =
        GetRandomPointOnNavMesh(playerTransform.position, distance);

        Zombie_Dissolve selectedItem = zombies[Random.Range(randomCountRange, zombies.Length)];
        Zombie_Dissolve zombie = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        zombielist.Add(zombie);

        zombie.onDeath += () => Destroy(zombie.gameObject, destroyCount);

        zombie.onDeath += () => cnt--;

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
        UIManager.instance.SafeHouse(true);
        yield return new WaitForSeconds(wateForSecond);
        UIManager.instance.SafeHouse(false);
        safe = true;
    }

}
