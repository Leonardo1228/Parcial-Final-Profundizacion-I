using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManagerPM : MonoBehaviour
{
    [Header("Tilemap")]
    public Tilemap wallTilemap;

    void Awake()
    {
        if (wallTilemap == null)
            Debug.LogError("GridManagerPM: wallTilemap no asignado.");
    }

    // Celda → Mundo
    public Vector3 CellToWorld(Vector3Int cell)
    {
        return wallTilemap.GetCellCenterWorld(cell);
    }

    // Mundo → Celda
    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        return wallTilemap.WorldToCell(worldPos);
    }

    // Verifica si celda es transitable
    public bool IsWalkable(Vector3Int cell)
    {
        return !wallTilemap.HasTile(cell);
    }
}


