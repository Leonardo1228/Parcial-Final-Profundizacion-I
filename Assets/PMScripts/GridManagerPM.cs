using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManagerPM : MonoBehaviour
{
    public Tilemap wallTilemap;

    // Convierte de celda → mundo
    public Vector3 CellToWorld(Vector3Int cell)
    {
        return wallTilemap.GetCellCenterWorld(cell);
    }

    // Convierte de mundo → celda
    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        return wallTilemap.WorldToCell(worldPos);
    }

    // Verifica si una celda es transitable
    public bool IsWalkable(Vector3Int cell)
    {
        return !wallTilemap.HasTile(cell);
    }
}

