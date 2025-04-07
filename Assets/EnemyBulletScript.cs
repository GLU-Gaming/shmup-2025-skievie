using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 50f;
    [SerializeField] public int damage = 1;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private LayerMask collisionMask;

    private void Start()
    {
        Destroy(GetComponent<Rigidbody>()); 
        GetComponent<Collider>().isTrigger = true;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
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