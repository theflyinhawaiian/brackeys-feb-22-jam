using Assets.Scripts;
using Assets.Scripts.Weapons;
using UnityEngine;

public class ProjectileEnemy : BaseEnemyController
{
    public Transform targetTransform;
    public float fireRate = 3f;

    private EquipmentSystem equipmentSystem;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;

        EnemyHealth = new HealthSystem(maxHealth);

        equipmentSystem = new EquipmentSystem(transform);
        equipmentSystem.Weapons.Add(new ProjectileEnemyWeapon());
    }

    private void Update()
    {
        equipmentSystem.TryUseActiveItem();
    }
}
