using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 10;
    public int height = 20;
    public Transform[,] grid;

    void Awake()
    {
        Instance = this;
        grid = new Transform[width, height];
    }

    public Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public bool InsideGrid(Vector2 pos)
    {
        return (int)pos.x >= 0 &&
               (int)pos.x < width &&
               (int)pos.y >= 0;
    }

    public bool IsValid(Transform tetromino)
    {
        foreach (Transform block in tetromino)
        {
            Vector2 pos = Round(block.position);

            if (!InsideGrid(pos))
                return false;

            if (pos.y < height && grid[(int)pos.x, (int)pos.y] != null)
                return false;
        }
        return true;
    }

    public void AddToGrid(Transform tetromino)
    {
        foreach (Transform block in tetromino)
        {
            Vector2 pos = Round(block.position);

            if (pos.y < height && InsideGrid(pos))
                grid[(int)pos.x, (int)pos.y] = block;
        }
    }
}
















