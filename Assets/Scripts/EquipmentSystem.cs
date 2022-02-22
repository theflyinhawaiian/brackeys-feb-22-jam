using UnityEngine;
using Assets.Scripts.Weapons;

namespace Assets.Scripts
{
    class EquipmentSystem
    {
        private BaseWeapon gun;
        private BaseWeapon laser;
        private Transform origin;

        public EquipmentSystem(Transform origin)
        {
            this.origin = origin;
            gun = new SimpleProjectileWeapon();
            laser = new HitscanWeapon();
        }

        public void TryUseActiveItem()
        {
            gun.Fire(origin);
        }
    }
}
