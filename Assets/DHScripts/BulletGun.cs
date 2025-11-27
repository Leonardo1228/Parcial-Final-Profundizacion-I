using UnityEngine;
public class BulletGun : MonoBehaviour
{
    void Start()
    {
        // simple animation or lifetime managed elsewhere
        Destroy(gameObject, 0.5f);
    }
}

