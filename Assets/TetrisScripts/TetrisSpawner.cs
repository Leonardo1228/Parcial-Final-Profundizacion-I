using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class TetrisSpawner : MonoBehaviour
{
    public Transform[] tetrominoes;
    private Vector3 spawnPoint = new Vector3(4.5f, 19f, 0); // ajustado para pivots centrados

    public Transform SpawnNext()
    {
        Debug.Log("TetrisSpawner.SpawnNext -> entrada. Stacktrace:\n" + new StackTrace(true).ToString());

        int r = Random.Range(0, tetrominoes.Length);
        Debug.Log("TetrisSpawner.SpawnNext -> instanciando índice: " + r);

        Transform piece = Instantiate(tetrominoes[r], spawnPoint, Quaternion.identity);

        if (!GridManager.Instance.IsValid(piece))
        {
            foreach (Transform b in piece)
                Debug.LogWarning("Bloque fuera de grid: " + b.position);

            Debug.LogWarning("TetrisSpawner.SpawnNext -> Spawn inválido: " + piece.name);
            Destroy(piece.gameObject);
            return null;
        }

        Debug.Log("TetrisSpawner.SpawnNext -> spawn OK: " + piece.name);
        return piece;
    }
}




















