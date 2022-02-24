using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class SimpleProjectileWeapon : BaseWeapon
    {
        private static GameObject _prefab;
        private static GameObject prefab
        {
            get
            {
                if (_prefab == null)
                    _prefab = Resources.Load<GameObject>("Prefabs/Bullet");

                return _prefab;
            }
        }

        public SimpleProjectileWeapon() {
            lastUseTime = Time.time;
        }

        public override void Fire(Transform origin)
        {
            if (!CanFire())
                return;

            lastUseTime = Time.time;

            GameObject p = Object.Instantiate(prefab, origin.position + (origin.up * 0.1f), origin.rotation);
            p.tag = target == TargetType.Enemy ? "PlayerBullet" : "EnemyBullet";

            var bullet = p.GetComponent<BulletDestroyer>();
            bullet.originTag = target == TargetType.Enemy ? "Player" : "Enemy";

            Rigidbody2D body = p.GetComponent<Rigidbody2D>();
            body.AddForce(origin.up * bulletVelocity, ForceMode2D.Impulse);
            Debug.Log($"Firing bullet with velocity: {body.velocity}");
        }
    }
}
