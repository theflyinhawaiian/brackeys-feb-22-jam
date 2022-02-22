using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class HitscanWeapon : BaseWeapon
    {
        public HitscanWeapon()
        {
            cooldown = 0.25f;
        }

        public override void Fire(Transform origin)
        {
            if (!CanFire())
                return;

            lastUseTime = Time.time;

            var hit = Physics2D.Raycast(origin.position, origin.TransformDirection(Vector3.up), Mathf.Infinity);
            // Does the ray intersect any objects excluding the player layer
            if (hit.transform != null)
            {
                Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.up) * hit.distance, Color.blue);

                var enemy = hit.transform.GetComponent<BaseEnemyController>();
                if(enemy != null)
                {
                    Debug.Log("AYOOO");
                }
            }
        }
    }
}
