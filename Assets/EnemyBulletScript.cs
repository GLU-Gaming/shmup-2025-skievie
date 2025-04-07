using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 50f;
    [SerializeField] public int damage = 1;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private LayerMask collisionMask; // Set to only include Player

    private void Start()
    {
        // Option 1: Simple movement (no physics)
        Destroy(GetComponent<Rigidbody>()); // Remove physics completely

        // Option 2: If you need physics for other reasons
        // GetComponent<Rigidbody>().isKinematic = true;

        GetComponent<Collider>().isTrigger = true;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Simple, reliable movement
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only collide with what's in collisionMask
        if (((1 << other.gameObject.layer) & collisionMask) != 0)
        {
            if (other.CompareTag("Player"))
            {
                GameManagement game = FindObjectOfType<GameManagement>();
                if (game != null) game.ReportPlayerHit(damage);
            }
            Destroy(gameObject);
        }
    }
}