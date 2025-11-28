using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    public Transform[] tetrominoes;
    private Vector3 spawnPoint = new Vector3(4.5f, 19f, 0);

    public Transform SpawnNext()
    {
        if (tetrominoes == null || tetrominoes.Length == 0)
        {
            Debug.LogError("TetrisSpawner -> No hay tetrominoes asignados!");
            return null;
        }

        if (GridManager.Instance == null)
        {
            Debug.LogError("TetrisSpawner -> GridManager.Instance es NULL");
            return null;
        }

        int r = Random.Range(0, tetrominoes.Length);
        Transform piece = Instantiate(tetrominoes[r], spawnPoint, Quaternion.identity);

        if (!GridManager.Instance.IsValid(piece))
        {
            Destroy(piece.gameObject);
            return null;
        }

        return piece;
    }
}





















