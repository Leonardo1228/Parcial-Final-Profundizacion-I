using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float rate = 0.2f;

    ShootPlayerActions input;
    float timer;

    void Awake()
    {
        input = new ShootPlayerActions();
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (input.Player.Shoot.ReadValue<float>() > 0.1f && timer >= rate)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            timer = 0f;
        }
    }
}


