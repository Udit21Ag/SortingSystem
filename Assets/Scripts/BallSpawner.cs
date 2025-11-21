using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] balls;
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
            SpawnBall();
            timer = 0f;
        }
    }

    void SpawnBall()
    {
        if (balls.Length == 0) return;

        int index = Random.Range(0, balls.Length);

        GameObject newBall = Instantiate(balls[index], spawnPoint.position, Quaternion.identity);

        BallLabel bl = newBall.GetComponent<BallLabel>();
        if (bl != null)
        {
            string raw = balls[index].name.ToLower();

            if (raw.Contains("red")) bl.label = "red";
            else if (raw.Contains("yellow")) bl.label = "yellow";
            else if (raw.Contains("green")) bl.label = "green";
            else bl.label = "unknown";
        }
    }

}