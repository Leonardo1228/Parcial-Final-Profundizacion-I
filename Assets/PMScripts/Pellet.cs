using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int value = 10;
    bool collected = false;

    public void Collect()
    {
        if (collected) return;
        collected = true;

        GameManagerPM.Instance?.AddScore(value);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PacController player = other.GetComponent<PacController>();
        if (player != null)
        {
            Collect();
        }
    }
}

