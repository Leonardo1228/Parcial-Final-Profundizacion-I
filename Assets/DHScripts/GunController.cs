using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject crosshair;
    public GameObject bulletPrefab;
    public Transform bulletParent;
    public InputActionReference aimAction; // asignar desde el inspector: DuckHuntControls - Player - Aim
    public InputActionReference fireAction; // DuckHuntControls - Player - Fire
    public float crosshairSpeed = 7f; // para joystick
    Vector2 crossPos;

    void OnEnable()
    {
        aimAction.action.Enable();
        fireAction.action.Enable();
        fireAction.action.performed += OnFire;
    }
    void OnDisable()
    {
        fireAction.action.performed -= OnFire;
        aimAction.action.Disable();
        fireAction.action.Disable();
    }

    void Start()
    {
        if (mainCam == null) mainCam = Camera.main;
        crossPos = crosshair.transform.position;
    }

    void Update()
    {
        Vector2 aim = aimAction.action.ReadValue<Vector2>();
        // Distinguish pointer vs stick: pointer returns position usually >0..screen
        if (Mouse.current != null && Mouse.current.position.ReadValue() != Vector2.zero && Mouse.current.leftButton != null)
        {
            // Use pointer absolute position
            Vector2 screenPos = aim;
            Vector3 world = mainCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
            crosshair.transform.position = new Vector3(world.x, world.y, 0);
            crossPos = world;
        }
        else
        {
            // treat as stick movement (relative)
            Vector3 pos = crosshair.transform.position;
            pos += new Vector3(aim.x, aim.y, 0) * crosshairSpeed * Time.deltaTime;
            // clamp to screen
            Vector3 vp = mainCam.WorldToViewportPoint(pos);
            vp.x = Mathf.Clamp01(vp.x);
            vp.y = Mathf.Clamp01(vp.y);
            crosshair.transform.position = mainCam.ViewportToWorldPoint(new Vector3(vp.x, vp.y, 10f));
            crossPos = crosshair.transform.position;
        }
    }

    void OnFire(InputAction.CallbackContext ctx)
    {
        Shoot();
    }

    void Shoot()
    {
        // Raycast at crosshair position
        Vector2 origin = mainCam.ScreenToWorldPoint(mainCam.WorldToScreenPoint(crossPos));
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
        if (hit.collider != null)
        {
            Duck d = hit.collider.GetComponentInParent<Duck>();
            if (d != null)
            {
                d.Hit();
                // optional: spawn bullet effect
            }
        }

        // optional: spawn a small bullet prefab for effect
        if (bulletPrefab != null)
        {
            var b = Instantiate(bulletPrefab, crossPos, Quaternion.identity, bulletParent);
            Destroy(b, 1f);
        }
    }
}

