using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    public float range = 3f;

    bool spawning = false;

    public void StartSpawning()
    {
        spawning = true;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    public void StopSpawning()
    {
        spawning = false;
        CancelInvoke();
    }

    void SpawnEnemy()
    {
        Debug.Log("Spawn ejecutado por: " + gameObject.name);

        if (!spawning) return;

        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab es NULL en: " + gameObject.name);
            return;
        }

        Vector3 pos = transform.position;
        pos.x += Random.Range(-range, range);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}


