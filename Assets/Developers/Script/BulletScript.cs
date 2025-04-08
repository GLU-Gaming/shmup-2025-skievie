using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] public int damage = 1;

    [Header("Components")]
    [SerializeField] private Collider bulletCollider;
    private Rigidbody rb;
    private GameManagement gameManager; // Added reference

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (bulletCollider == null) bulletCollider = GetComponent<Collider>();

        rb.isKinematic = true;
        gameManager = FindAnyObjectByType<GameManagement>(); 

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null) enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void DestroyBullet()
    {
        if (bulletCollider != null) bulletCollider.enabled = false;
        Destroy(gameObject);
    }
}
