using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private int scoreAmount;
<<<<<<< Updated upstream
    [SerializeField] protected int HPamount;
    [SerializeField] protected float fireRate;
=======
    [SerializeField] protected int HPamount; 
    [SerializeField] protected float fireRate; 
>>>>>>> Stashed changes
    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private GameObject EnemyBulletSpawnPoint;
    [SerializeField] private float destroyTime = 10f;

    private float fireRateTimer;
    protected GameManagement game;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Acceleration);
<<<<<<< Updated upstream
        game = FindObjectOfType<GameManagement>();
        fireRateTimer = fireRate;

        Invoke(nameof(DestroyEnemy), destroyTime);
=======
        game = FindAnyObjectByType<GameManagement>();
        fireRateTimer = fireRate;
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
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
=======
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
