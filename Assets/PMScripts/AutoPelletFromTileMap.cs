using UnityEngine;

[RequireComponent(typeof(GridManagerPM))]
public class AutoPelletFromTilemap : MonoBehaviour
{
    [Header("References")]
    public GridManagerPM grid;
    public Pellet pelletPrefab;

    [Header("Spawn Area")]
    public Vector2Int min;
    public Vector2Int max;

    void Awake()
    {
        if (grid == null) grid = GetComponent<GridManagerPM>();
        if (grid == null)
        {
            Debug.LogError("AutoPelletFromTilemap: GridManagerPM no encontrado.");
            enabled = false;
            return;
        }

        if (pelletPrefab == null)
        {
            Debug.LogError("AutoPelletFromTilemap: pelletPrefab no asignado.");
            enabled = false;
        }
    }

    void Start()
    {
        SpawnPellets();
    }

    void SpawnPellets()
    {
        for (int x = min.x; x <= max.x; x++)
        {
            for (int y = min.y; y <= max.y; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (grid.IsWalkable(cell))
                {
                    Vector3 pos = grid.CellToWorld(cell);
                    Instantiate(pelletPrefab, pos, Quaternion.identity);
                }
            }
        }
    }
}


