using Assets.Scripts;
using Assets.Scripts.Weapons;
using UnityEngine;

public class ProjectileEnemy : BaseEnemyController
{
    public Transform targetTransform;
    public float fireRate = 3f;

    public float orbitRadius = 5f;
    public float orbitBuffer = 0.05f;

    public float rotationSpeed = 0.5f;

    private EquipmentSystem equipmentSystem;

    private bool shouldOrbit = false;

    void Start()
    {
        EnemyHealth = new HealthSystem(maxHealth);

        equipmentSystem = new EquipmentSystem(transform);
        equipmentSystem.Weapons.Add(new ProjectileEnemyWeapon());

        prototypeResourceName = "ProjectileEnemy";

        if (targetTransform == null)
            targetTransform = GameManager.GetPlayerTransform();
    }

    private void Update()
    {
        equipmentSystem.TryUseActiveItem();
    }

    void FixedUpdate()
    {
        var distance = Vector3.Distance(transform.position, targetTransform.position);

        if (distance > orbitRadius + orbitBuffer)
        {
            target = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * maxVelocity);
            transform.position = target;
        }
        else if (distance < orbitRadius - orbitBuffer)
        {
            target = Vector3.MoveTowards(transform.position, targetTransform.position, -Time.deltaTime * maxVelocity); 
            transform.position = target;
        }
        else
        {
            transform.RotateAround(targetTransform.position, Vector3.forward, rotationSpeed);
        }

        var diff = targetTransform.position - transform.position;

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
