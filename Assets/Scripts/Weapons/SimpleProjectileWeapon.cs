using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class SimpleProjectileWeapon : BaseWeapon
    {
        public SimpleProjectileWeapon()
        {
            prefab = Resources.Load("Prefabs/Bullet") as GameObject;
        }

        public override void Fire(Transform origin)
        {
            if (!CanFire())
                return;

            lastUseTime = Time.time;

            GameObject p = Object.Instantiate(prefab, origin.position, origin.rotation);
            Rigidbody2D body = p.GetComponent<Rigidbody2D>();
            body.AddForce(origin.up * bulletVelocity, ForceMode2D.Impulse);
        }
    }
}
