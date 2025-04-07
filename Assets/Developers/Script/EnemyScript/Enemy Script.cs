using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 15f; // Increased speed (adjust as needed)
    [SerializeField] private float destroyTime = 15f; // Increased lifetime before despawning

    [Header("Combat Settings")]
    [SerializeField] protected int scoreAmount = 100;
    [SerializeField] protected int HPamount = 3;

    [Header("Shooting Settings")]
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float shootingRange = 20f; // Increased range to shoot even when off-screen
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject bulletPrefab;

    protected Rigidbody rb;
    protected GameManagement gameManager;
    protected Transform player;
    private float nextFireTime;

    public virtual void Activate() { /* Base activation logic */ }
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

        // Check if player is within range (even if off-screen)
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return (distanceToPlayer <= shootingRange && Time.time >= nextFireTime);
    }

    protected void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); 

        nextFireTime = Time.time + fireRate;
    }

    protected virtual void ApplyInitialMovement()
    {
        if (rb != null)
        {
            // Move left at a constant speed (no physics drag)
            rb.velocity = Vector3.left * moveSpeed;
        }
    }

    protected virtual void OnBecameInvisible()
    {
        // Only despawn if completely off-screen (left side)
        if (transform.position.x < -20f) // Adjust based on your camera view
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
