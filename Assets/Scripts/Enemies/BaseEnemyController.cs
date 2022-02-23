using Assets.Scripts.Enemies;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour, IEnemy
{
    public float maxVelocity = 5;
    public float changeTargetInterval = 1.5f;

    private Rigidbody2D body;
    private HealthSystem EnemyHealth;

    private Vector3 target;
    private float lastTargetChangeTime;

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
        target = GenerateNewTarget();
    }

    private void Update()
    {
        if(Time.time - lastTargetChangeTime > changeTargetInterval)
        {
            target = GenerateNewTarget();
            lastTargetChangeTime = Time.time;
        }
    }

    Vector3 GenerateNewTarget() => Random.insideUnitCircle * 5;

    void FixedUpdate()
    {
        var diff = transform.position - target;
        var moveDirection = new Vector2(diff.x, diff.y);

        body.position = body.position - moveDirection.normalized * maxVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            EnemyHealth.Damage(1);

            if (EnemyHealth.GetHealth() == 0)
            {
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
