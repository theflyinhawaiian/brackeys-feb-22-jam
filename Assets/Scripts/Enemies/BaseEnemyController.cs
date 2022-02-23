using Assets.Scripts;
using Assets.Scripts.Enemies;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour, IEnemy
{
    public float maxVelocity = 0.05f;
    public float changeTargetInterval = 1.5f;

    private Rigidbody2D body;
    private HealthSystem EnemyHealth;

    private Vector3 target;
    private float lastTargetChangeTime;
    private bool needsTarget = true;

    public delegate void EnemyDeath();
    public event EnemyDeath OnDeath;

    private GameObject _prototype;
    protected string prototypeResourceName = "Enemy";

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;

        EnemyHealth = new HealthSystem(5);

        lastTargetChangeTime = Time.time;
    }

    private void Update()
    {
        if(needsTarget || Time.time - lastTargetChangeTime > changeTargetInterval)
        {
            target = GenerateNewTarget();
            lastTargetChangeTime = Time.time;
            needsTarget = false;
        }
    }

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
        if (collision.gameObject.CompareTag("Bullet"))
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
