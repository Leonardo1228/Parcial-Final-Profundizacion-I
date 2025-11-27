using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoPelletFromTilemap : MonoBehaviour
{
    public GridManagerPM grid;
    public Pellet pelletPrefab;

    public Vector2Int min;
    public Vector2Int max;

    void Start()
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

