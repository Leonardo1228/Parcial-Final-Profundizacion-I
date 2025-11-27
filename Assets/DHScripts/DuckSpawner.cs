using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duckPrefab;
    public float spawnInterval = 2f;
    public float spawnTimer = 0f;
    public float minY = -1f, maxY = 3f;
    public float minSpeed = 1.5f, maxSpeed = 3f;
    public float screenEdgeOffset = 0.5f;
    public Camera mainCam;

    void Start()
    {
        if (mainCam == null) mainCam = Camera.main;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnDuck();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnDuck()
    {
        bool fromLeft = Random.value > 0.5f;
        float y = Random.Range(minY, maxY);
        Vector3 spawnWorld;
        Vector2 velocity;
        float speed = Random.Range(minSpeed, maxSpeed);

        // spawn just off-screen horizontally
        if (fromLeft)
        {
            spawnWorld = mainCam.ViewportToWorldPoint(new Vector3(0 - screenEdgeOffset, 0.5f, 10f));
            spawnWorld.y = y;
            velocity = new Vector2(speed, Random.Range(-0.5f, 0.5f));
        }
        else
        {
            spawnWorld = mainCam.ViewportToWorldPoint(new Vector3(1 + screenEdgeOffset, 0.5f, 10f));
            spawnWorld.y = y;
            velocity = new Vector2(-speed, Random.Range(-0.5f, 0.5f));
        }

        var go = Instantiate(duckPrefab, spawnWorld, Quaternion.identity);
        var duck = go.GetComponent<Duck>();
        duck.Init(velocity);
    }

    void OnNextRound()
    {
        // increase difficulty a little
        spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.2f);
        minSpeed += 0.2f;
        maxSpeed += 0.2f;
    }
}

