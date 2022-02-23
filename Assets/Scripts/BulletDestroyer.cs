using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public GameObject hitEffect;
    void Start()
    {
        Destroy(gameObject, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
