using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class BaseWeapon : IItemWithCooldown
    {
        public float bulletVelocity = 20f;

        protected float cooldown = 0.25f;
        protected float lastUseTime = -1000;

        protected TargetType target = TargetType.Enemy;

        public BaseWeapon() { }

        public virtual void Fire(Transform origin) { }

        protected bool CanFire() => Time.time > lastUseTime + cooldown;

        public float GetLastUseTime() => lastUseTime;

        public float GetCooldown() => cooldown;
    }

}