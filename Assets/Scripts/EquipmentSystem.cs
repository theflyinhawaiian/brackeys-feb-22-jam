using UnityEngine;
using Assets.Scripts.Weapons;
using System.Collections.Generic;

namespace Assets.Scripts
{
    class EquipmentSystem
    {
        private Transform origin;

        public List<BaseWeapon> Weapons { get; private set; } = new List<BaseWeapon>();

        private int selectedWeaponIndex;

        public EquipmentSystem(Transform origin)
        {
            this.origin = origin;
        }

        public void TryUseActiveItem()
        {
            if (selectedWeaponIndex < Weapons.Count)
                Weapons[selectedWeaponIndex].Fire(origin);
        }

        public bool SetSelectedWeapon(int index)
        {
            if (index < 0 || index >= Weapons.Count)
                return false;

            selectedWeaponIndex = index;
            return true;
        }

        public void CycleSelectedWeaponForward()
        {
            if (selectedWeaponIndex == Weapons.Count - 1)
            {
                selectedWeaponIndex = 0;
                return;
            }

            selectedWeaponIndex++;
        }

        public void CycleSelectedWeaponBackward()
        {
            if (selectedWeaponIndex == 0)
            {
                selectedWeaponIndex = Weapons.Count - 1;
                return;
            }

            selectedWeaponIndex--;
        }
    }
}
