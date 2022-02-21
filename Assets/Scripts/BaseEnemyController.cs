using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    public float maxVelocity = 5;
    public float changeTargetInterval = 1.5f;

    private Rigidbody2D body;
    private HealthSystem EnemyHealth;

    private Vector3 target;
    private float lastTargetChangeTime;

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
            Debug.Log("Health: " + EnemyHealth.GetHealth());

            if (EnemyHealth.GetHealth() == 0)
            {
                Destroy(gameObject);
            }
        }
    }







}
