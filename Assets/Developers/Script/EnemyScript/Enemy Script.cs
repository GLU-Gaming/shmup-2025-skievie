using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject EnemyTest;
    [SerializeField] private float moveSpeed = 8;

    public GameManagement game;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Force); // movement van enemy
    }
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        Shooter projectile = collision.gameObject.GetComponent<Shooter>();
        PlaneScrip player = collision.gameObject.GetComponent<PlaneScrip>();
        if (projectile != null)
        {
            game.RemoveEnemy(gameObject);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (player != null)
        {
            // hit Player
        }
    }
}
