using UnityEngine;
using Assets.Scripts;

public class BaseWeapon : IItemWithCooldown
{
    public GameObject prefab;
    public float emitForce = 20f;

    public float lastUseTime = -1000;
    public float cooldown = 0.25f;

    public BaseWeapon()
    {
        prefab = Resources.Load("Prefabs/Bullet") as GameObject;
    }

    public void FireBullet(Transform origin)
    {
        if (lastUseTime + cooldown > Time.time)
            return;

        GameObject p = Object.Instantiate(prefab, origin.position, origin.rotation);
        Rigidbody2D body = p.GetComponent<Rigidbody2D>();
        body.AddForce(origin.up * emitForce, ForceMode2D.Impulse);
        lastUseTime = Time.time;
    }

    public float GetLastUseTime() => lastUseTime;

    public float GetCooldown() => cooldown;
}
