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

    public int animalCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        if (animals.Count <= 0)
            SpawnAnimal();
    }

    private void SpawnAnimal()
    {
        for (int i = 0; i < 4; i++)
            CreateAnimal();

        for(int i = 0; i < 2; i ++)
        {
            CreateAnimal1();
        }
    }

    private void CreateAnimal()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Animal animal = Instantiate(animalPrefabs, spawnPoint.position, spawnPoint.rotation);

        animals.Add(animal);

        animal.onDeath += () => animals.Remove(animal);

        animal.onDeath += () => Destroy(animal.gameObject, 5f);
    }

    private void CreateAnimal1()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        AnimalChicken animal = Instantiate(animalPrefabs1, spawnPoint.position, spawnPoint.rotation);

        animals1.Add(animal);

        animal.onDeath += () => animals1.Remove(animal);

        animal.onDeath += () => Destroy(animal.gameObject, 5f);

    }

}
