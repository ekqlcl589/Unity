using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieParade : MonoBehaviour
{
    public Zombie_Dissolve[] zombies;
    public Transform playerTransform;

    private List<Zombie_Dissolve> zombielist = new List<Zombie_Dissolve>();
    public float distance = 20f;
    private int cnt = 50;
    private bool safe = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.last && !GameManager.instance.isGameover && zombielist.Count <= 50)
        {
           Spawn();
        }

        if (cnt <= 35 && !safe)
        {
            GameManager.instance.clear = true;
            StartCoroutine(Cor_ShowSafeText5Sec());
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition =
        GetRandomPointOnNavMesh(playerTransform.position, distance);

        Zombie_Dissolve selectedItem = zombies[Random.Range(0, zombies.Length)];
        Zombie_Dissolve zombie = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        zombielist.Add(zombie);

        zombie.onDeath += () => Destroy(zombie.gameObject, 5f);

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
        yield return new WaitForSeconds(5f);
        UIManager.instance.SafeHouse(false);
        safe = true;
    }

}
