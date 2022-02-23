using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public string originTag;
    public GameObject hitEffect;

    void Start()
    {
        Destroy(gameObject, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == originTag)
            return;

        Debug.Log("Bullet Dying!");
        Destroy(gameObject);
    }
}
