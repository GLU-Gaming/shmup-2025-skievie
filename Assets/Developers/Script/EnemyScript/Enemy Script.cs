using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private int scoreAmount;
    [SerializeField] protected int HPamount;
    [SerializeField] protected float fireRate;
    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private GameObject EnemyBulletSpawnPoint;
    [SerializeField] private float destroyTime = 10f;

    private float fireRateTimer;
    protected GameManagement game;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Acceleration); // movement van enemy

        Invoke(nameof(DestroyEnemy), destroyTime);

        game = FindAnyObjectByType<GameManagement>();
        fireRateTimer = fireRate;

    }

    protected virtual void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if (fireRateTimer <= 0)
        {
            FireEnemyBullet();
            fireRateTimer = fireRate;
        }
    }
    private void DestroyEnemy()
    {
        if (game != null)
        {
            game.RemoveEnemy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        HPamount -= damage;
        if (HPamount <= 0)
        {
            game.AddScore(scoreAmount);
            game.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void Activate()
    {
        // This method can be overridden by derived classes
    }

    private void FireEnemyBullet()
    {
        if (EnemyBullet != null && EnemyBulletSpawnPoint != null)
        {
            Instantiate(EnemyBullet, EnemyBulletSpawnPoint.transform.position, EnemyBullet.transform.rotation);
        }
    }
}
