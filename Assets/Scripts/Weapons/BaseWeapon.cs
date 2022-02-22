using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts.Weapons
{
    public abstract class BaseWeapon : IItemWithCooldown
    {
        public GameObject prefab;
        public float bulletVelocity = 20f;

        protected float cooldown = 0.25f;
        protected float lastUseTime = -1000;

        public BaseWeapon() { }

        public virtual void Fire(Transform origin) { }

        protected bool CanFire() => Time.time > lastUseTime + cooldown;

        public float GetLastUseTime() => lastUseTime;

        public float GetCooldown() => cooldown;
    }

}