using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class GameManagerT : MonoBehaviour
{
    public static GameManagerT Instance;

    public TetrisSpawner spawner;

    Transform currentPiece = null;
    bool spawningLocked = false;
    int lastSpawnFrame = -1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("GameManager duplicado detectado y destruido: " + this.name);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Debug.Log(">>> GameManager.Start ejecutado en objeto: " + gameObject.name);
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        if (Time.frameCount == lastSpawnFrame) return;
        if (spawningLocked) return;
        if (currentPiece != null) return;

        lastSpawnFrame = Time.frameCount;
        spawningLocked = true;

        Transform spawned = spawner.SpawnNext();

        if (spawned != null)
        {
            currentPiece = spawned;
            Debug.Log("GameManager.SpawnPiece -> pieza creada: " + spawned.name);
        }
        else
        {
            Debug.LogWarning("GameManager.SpawnPiece -> SpawnNext devolvió null. Revisa prefab y GridManager!");
        }

        spawningLocked = false;
    }

    public void PieceLocked()
    {
        currentPiece = null;
        SpawnPiece();
    }
}























