using UnityEngine;

namespace Assets.Scripts
{
    class EquipmentSystem
    {
        private BaseWeapon gun;
        private Transform origin;

        public EquipmentSystem(Transform origin)
        {
            this.origin = origin;
            gun = new BaseWeapon();
        }

        public void TryUseActiveItem()
        {
            gun.FireBullet(origin);
        }
    }
}
