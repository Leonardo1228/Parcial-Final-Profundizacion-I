using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float rate = 0.2f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && timer >= rate)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            timer = 0f;
        }
    }
}

