using Assets.Scripts;
using Assets.Scripts.Enemies;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour, IEnemy
{
    public delegate void EnemyDeath();
    public event EnemyDeath OnDeath;

    protected HealthSystem health;
    public EnemyType Type { get; set; } = EnemyType.Basic;

    public float maxVelocity = 0.05f;
    public int startingHealth = 5;

    protected Vector3 target;
    protected bool needsTarget = true;

    private float changeTargetInterval = 1.5f;
    private float lastTargetChangeTime;
    
    public int MaxHealth => health.GetMaxHealth();

    public int CurrentHealth => health.GetHealth();

    void Start()
    {
        lastTargetChangeTime = Time.time;
    }

    private void Update()
    {
        if (CheckNeedsTarget())
        {
            target = GenerateNewTarget();
            lastTargetChangeTime = Time.time;
            needsTarget = false;
        }
    }

    protected virtual bool CheckNeedsTarget() => needsTarget || Time.time - lastTargetChangeTime > changeTargetInterval;

    Vector3 GenerateNewTarget()
    {
        var rand = transform.position + (Random.insideUnitSphere.normalized * 100);
        rand.z = Constants.EntityZValue;
        return rand;
    }

    void FixedUpdate()
    {
        var move = Vector3.MoveTowards(transform.position, target, Time.deltaTime * maxVelocity);
        transform.position = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health.Damage(1);

            if (health.GetHealth() == 0)
            {
                if(OnDeath != null)
                    OnDeath.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void Configure(int currentHealth, int maxHealth)
    {
        health = new HealthSystem(maxHealth);
        health.SetHealth(currentHealth);
    }
}
