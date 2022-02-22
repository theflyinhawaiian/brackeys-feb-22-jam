using UnityEngine;

namespace Assets.Scripts
{
    class EquipmentSystem
    {
        private BaseWeapon gun;
        private HitscanWeapon laser;
        private Transform origin;

        public EquipmentSystem(Transform origin)
        {
            this.origin = origin;
            gun = new BaseWeapon();
            laser = new HitscanWeapon();
        }

        public void TryUseActiveItem()
        {
            //gun.FireBullet(origin);
            laser.Fire(origin);
        }
    }
}
