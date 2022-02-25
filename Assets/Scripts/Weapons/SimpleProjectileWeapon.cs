using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class SimpleProjectileWeapon : BaseWeapon
    {
        protected ProjectileType projectileType;

        public SimpleProjectileWeapon() {
            lastUseTime = Time.time;
            projectileType = ProjectileType.Basic;
        }

        public override void Fire(Transform origin)
        {
            if (!CanFire())
                return;

            lastUseTime = Time.time;

            GameObject p = Object.Instantiate(ResourceManager.ProjectilePrefabs[projectileType], origin.position + (origin.up * 0.1f), origin.rotation); ;
            p.tag = target == TargetType.Enemy ? "PlayerBullet" : "EnemyBullet";

            var bullet = p.GetComponent<BulletDestroyer>();
            bullet.originTag = target == TargetType.Enemy ? "Player" : "Enemy";

            Rigidbody2D body = p.GetComponent<Rigidbody2D>();
            body.AddForce(origin.up * bulletVelocity, ForceMode2D.Impulse);
        }
    }
}
