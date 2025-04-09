using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 15f;
    [SerializeField] private float destroyTime = 15f;

    [Header("Combat Settings")]
    [SerializeField] protected int scoreAmount = 100;
    [SerializeField] protected int HPamount = 3;

    [Header("Shooting Settings")]
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float shootingRange = 20f;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject bulletPrefab;

    protected Rigidbody rb;
    protected GameManagement gameManager;
    protected Transform player;
    private float nextFireTime;

    public virtual void Activate() { }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindAnyObjectByType<GameManagement>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected virtual void Start()
    {
        ApplyInitialMovement();
        Invoke(nameof(DestroyEnemy), destroyTime);
    }

    protected virtual void Update()
    {
        if (CanShoot()) Shoot();
    }

    protected bool CanShoot()
    {
        if (player == null || bulletPrefab == null || firePoint == null)
            return false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return (distanceToPlayer <= shootingRange && Time.time >= nextFireTime);
    }

    protected void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.rotation = Quaternion.Euler(-90, 180, 0);

        nextFireTime = Time.time + fireRate;
    }

    protected virtual void ApplyInitialMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.left * moveSpeed;
        }
    }

    protected virtual void OnBecameInvisible()
    {
        if (transform.position.x < -20f)
        {
            DestroyEnemy();
        }
    }

    public void TakeDamage(int damage)
    {
        HPamount -= damage;
        if (HPamount <= 0)
        {
            if (gameManager != null)
                gameManager.EnemyDied(gameObject, scoreAmount);
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        if (gameManager != null)
            gameManager.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
