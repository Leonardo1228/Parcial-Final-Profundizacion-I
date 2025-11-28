using UnityEngine;
using System.Collections;

public class Tetromino : MonoBehaviour
{
    public float fallTime = 0.5f;
    float timer = 0f;

    bool justSpawned = true;

    void Update()
    {
        if (justSpawned)
        {
            justSpawned = false;
            return;
        }

        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) Move(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) Move(Vector3.right);
        if (Input.GetKeyDown(KeyCode.UpArrow)) Rotate();
        if (Input.GetKey(KeyCode.DownArrow)) MoveDown();
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(HardDropSmooth());

        if (timer >= fallTime)
        {
            timer = 0;
            MoveDown();
        }
    }

    void Move(Vector3 dir)
    {
        if (GridManager.Instance == null) return;

        transform.position += dir;

        if (!GridManager.Instance.IsValid(transform))
            transform.position -= dir;
    }

    void MoveDown()
    {
        if (GridManager.Instance == null) return;

        transform.position += Vector3.down;

        if (!GridManager.Instance.IsValid(transform))
        {
            transform.position += Vector3.up;
            Lock();
        }
    }

    void Rotate()
    {
        if (GridManager.Instance == null) return;

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
        if (GridManager.Instance == null || GameManagerT.Instance == null)
            return;

        GridManager.Instance.AddToGrid(transform);
        GameManagerT.Instance.PieceLocked();
        enabled = false;
    }
}












