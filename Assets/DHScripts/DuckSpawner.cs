using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    [Header("Duck")]
    public GameObject duckPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    float spawnTimer;
    bool active;

    public float minY = -1f, maxY = 3f;
    public float minSpeed = 1.5f, maxSpeed = 3f;

    public float screenEdgeOffset = 0.1f;
    public Camera mainCam;

    void Start()
    {
        if (!mainCam) mainCam = Camera.main;
    }

    void Update()
    {
        if (!active) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnDuck();
            spawnTimer = spawnInterval;
        }
    }

    // 🔥 LLAMADO DESDE EL GAME MANAGER
    public void StartSpawning()
    {
        active = true;
        spawnTimer = 0f;
    }

    // 🔥 LLAMADO DESDE EL GAME MANAGER
    public void StopSpawning()
    {
        active = false;
    }

    // 🔥 LLAMADO DESDE EL GAME MANAGER
    public void IncreaseDifficulty()
    {
        spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.2f);
        minSpeed += 0.2f;
        maxSpeed += 0.2f;
    }

    void SpawnDuck()
    {
        bool fromLeft = Random.value > 0.5f;
        float y = Random.Range(minY, maxY);
        float speed = Random.Range(minSpeed, maxSpeed);

        Vector3 spawnWorld;
        Vector2 velocity;

        if (fromLeft)
        {
            spawnWorld = mainCam.ViewportToWorldPoint(
                new Vector3(0 - screenEdgeOffset, 0.5f, 10f)
            );
            velocity = new Vector2(speed, Random.Range(-0.5f, 0.5f));
        }
        else
        {
            spawnWorld = mainCam.ViewportToWorldPoint(
                new Vector3(1 + screenEdgeOffset, 0.5f, 10f)
            );
            velocity = new Vector2(-speed, Random.Range(-0.5f, 0.5f));
        }

        spawnWorld.y = y;

        var go = Instantiate(duckPrefab, spawnWorld, Quaternion.identity);
        var duck = go.GetComponent<Duck>();
        duck.Init(velocity);
    }
}


