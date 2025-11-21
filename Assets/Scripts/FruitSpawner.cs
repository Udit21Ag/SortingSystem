using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruits;   // apple, orange, banana prefabs
    public float spawnInterval = 2f;
    public Transform spawnPoint;

    [HideInInspector]
    public bool canSpawn = true;

    private float timer = 0f;

    void Update()
    {
        if (!canSpawn) return;   // STOP SPAWNING when belt stops or sorting

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFruit();
            timer = 0f;
        }
    }

    void SpawnFruit()
    {
        if (fruits.Length == 0) return;

        int index = Random.Range(0, fruits.Length);
        Instantiate(fruits[index], spawnPoint.position, Quaternion.identity);
    }
}