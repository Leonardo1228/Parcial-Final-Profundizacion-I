using UnityEngine;
using System.Collections;

public class Tetromino : MonoBehaviour
{
    public float fallTime = 0.5f;
    float timer = 0f;

    bool justSpawned = true;  // <-- FIX para evitar doble spawn

    void Update()
    {
        // Evitar movimiento el primer frame
        if (justSpawned)
        {
            justSpawned = false;
            return;
        }

        timer += Time.deltaTime;

        // Input horizontal
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector3.left);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector3.right);

        // Rotación
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Rotate();

        // Soft drop (solo baja 1 cuadro a la vez)
        if (Input.GetKey(KeyCode.DownArrow))
            MoveDown();

        // Hard drop
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(HardDropSmooth());

        // Caída automática
        if (timer >= fallTime)
        {
            timer = 0;
            MoveDown();
        }
    }

    void Move(Vector3 dir)
    {
        transform.position += dir;

        if (!GridManager.Instance.IsValid(transform))
            transform.position -= dir;
    }

    void MoveDown()
    {
        transform.position += Vector3.down;

        if (!GridManager.Instance.IsValid(transform))
        {
            transform.position += Vector3.up;
            Lock();
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 0, 90);

        if (!GridManager.Instance.IsValid(transform))
            transform.Rotate(0, 0, -90);
    }

    IEnumerator HardDropSmooth()
    {
        while (true)
        {
            transform.position += Vector3.down;

            if (!GridManager.Instance.IsValid(transform))
            {
                transform.position += Vector3.up;
                Lock();
                yield break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    void Lock()
    {
        GridManager.Instance.AddToGrid(transform);
        GameManagerT.Instance.PieceLocked();
        enabled = false;
    }
}










