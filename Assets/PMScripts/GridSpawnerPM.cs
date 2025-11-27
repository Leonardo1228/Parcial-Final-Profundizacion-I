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

    PacController currentPlayer;
    List<GhostAI> ghosts = new List<GhostAI>();
    List<Pellet> pellets = new List<Pellet>();

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

        Vector3 worldPos = grid.CellToWorld(playerSpawnCell);

        currentPlayer = Instantiate(playerPrefab, worldPos, Quaternion.identity);
        GameManagerPM.Instance.player = currentPlayer;
    }

    // ================= GHOSTS =================
    void SpawnGhosts()
    {
        ClearGhosts();

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

