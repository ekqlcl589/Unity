using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public Animal animalPrefabs; // 배열로 여러개 만들어서 생성 하는거 생각
    public AnimalChicken animalPrefabs1; // 배열로 여러개 만들어서 생성 하는거 생각

    public Transform[] spawnPoints;


    private List<Animal> animals = new List<Animal>();
    private List<AnimalChicken> animals1 = new List<AnimalChicken>();

    private const float destroyCount = 5f;

    private const int zeroPoint = 0;

    private const int createNum = 2;
    private const int createNum1 = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.IsGameover)
        {
            return;
        }

        if (animals.Count <= zeroPoint)
            SpawnAnimal();
    }

    private void SpawnAnimal()
    {
        for (int i = 0; i < createNum1; i++)
            CreateAnimal();

        for (int i = 0; i < createNum; i++)
            CreateAnimal1();
    }

    private void CreateAnimal()
    {
        Transform spawnPoint = spawnPoints[Random.Range(zeroPoint, spawnPoints.Length)];

        Animal animal = Instantiate(animalPrefabs, spawnPoint.position, spawnPoint.rotation);

        animals.Add(animal);

        animal.onDeath += () => animals.Remove(animal);

        animal.onDeath += () => Destroy(animal.gameObject, destroyCount);
    }

    private void CreateAnimal1()
    {
        Transform spawnPoint = spawnPoints[Random.Range(zeroPoint, spawnPoints.Length)];

        AnimalChicken animal = Instantiate(animalPrefabs1, spawnPoint.position, spawnPoint.rotation);

        animals1.Add(animal);

        animal.onDeath += () => animals1.Remove(animal);

        animal.onDeath += () => Destroy(animal.gameObject, destroyCount);

    }

}
