using Assets.Scripts;
using Assets.Scripts.Enemies;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour, IEnemy
{
    public float maxVelocity = 0.05f;
    public int maxHealth = 5;
    public delegate void EnemyDeath();
    public event EnemyDeath OnDeath;

    protected HealthSystem EnemyHealth;
    protected Rigidbody2D body;
    protected string prototypeResourceName = "Enemy";
    protected Vector3 target;
    protected bool needsTarget = true;

    private GameObject _prototype;

    private float changeTargetInterval = 1.5f;
    private float lastTargetChangeTime;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;

        EnemyHealth = new HealthSystem(maxHealth);

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
        var rand = Random.insideUnitSphere.normalized * 5;
        rand.z = Constants.EntityZValue;
        return rand.normalized;
    }

    void FixedUpdate()
    {
        body.position += (Vector2)target.normalized * maxVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            EnemyHealth.Damage(1);

            if (EnemyHealth.GetHealth() == 0)
            {
                if(OnDeath != null)
                    OnDeath.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public GameObject GetPrototype()
    {
        if (_prototype == null)
            _prototype = Resources.Load<GameObject>($"Prefabs/{prototypeResourceName}");

        return _prototype;
    }
}
