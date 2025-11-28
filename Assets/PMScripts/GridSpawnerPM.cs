using UnityEngine;
using System.Collections.Generic;

public class GridSpawnerPM : MonoBehaviour
{
    [Header("Grid")]
    public GridManagerPM grid;

    [Header("Player")]
    public PacController playerPrefab;
    public Vector3Int playerSpawnCell;

    [Header("Ghosts")]
    public GhostAI ghostPrefab;
    public List<Vector3Int> ghostSpawnCells;

    [Header("Pellet")]
    public Pellet pelletPrefab;
    public List<Vector3Int> pelletSpawnCells;

    private PacController currentPlayer;
    private List<GhostAI> ghosts = new List<GhostAI>();
    private List<Pellet> pellets = new List<Pellet>();

    void Awake()
    {
        if (grid == null)
            Debug.LogError("GridSpawnerPM: grid no asignado.");
    }

    void Start()
    {
        SpawnAll();
    }

    public void SpawnAll()
    {
        SpawnPlayer();
        SpawnGhosts();
        SpawnPellets();
    }

    // ================= PLAYER =================
    public void SpawnPlayer()
    {
        if (currentPlayer != null)
            Destroy(currentPlayer.gameObject);

        if (playerPrefab == null)
        {
            Debug.LogError("GridSpawnerPM: playerPrefab no asignado.");
            return;
        }

        Vector3 worldPos = grid.CellToWorld(playerSpawnCell);
        currentPlayer = Instantiate(playerPrefab, worldPos, Quaternion.identity);
        GameManagerPM.Instance.player = currentPlayer;
    }

    // ================= GHOSTS =================
    void SpawnGhosts()
    {
        ClearGhosts();
        if (ghostPrefab == null)
        {
            Debug.LogWarning("GridSpawnerPM: ghostPrefab no asignado.");
            return;
        }

        foreach (var cell in ghostSpawnCells)
        {
            Vector3 pos = grid.CellToWorld(cell);
            GhostAI g = Instantiate(ghostPrefab, pos, Quaternion.identity);
            ghosts.Add(g);
        }
    }

    public void ResetGhosts()
    {
        ClearGhosts();
        SpawnGhosts();
    }

    void ClearGhosts()
    {
        foreach (var g in ghosts)
            if (g != null) Destroy(g.gameObject);
        ghosts.Clear();
    }

    // ================= PELLETS =================
    void SpawnPellets()
    {
        ClearPellets();
        if (pelletPrefab == null)
        {
            Debug.LogWarning("GridSpawnerPM: pelletPrefab no asignado.");
            return;
        }

        foreach (var cell in pelletSpawnCells)
        {
            Vector3 pos = grid.CellToWorld(cell);
            Pellet p = Instantiate(pelletPrefab, pos, Quaternion.identity);
            pellets.Add(p);
        }
    }

    void ClearPellets()
    {
        foreach (var p in pellets)
            if (p != null) Destroy(p.gameObject);
        pellets.Clear();
    }
}


