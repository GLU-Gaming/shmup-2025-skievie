using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject EnemyTest;
    [SerializeField] private float moveSpeed = 8;

    public GameManagement game; // script aan script 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Force); // movement van enemy

        game = FindAnyObjectByType<GameManagement>();
    }
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision) // collide om de enemy te verwijderen, geldt ook voor de kogel 
    {
        Shooter projectile = collision.gameObject.GetComponent<Shooter>(); // kogel 
        PlaneScrip player = collision.gameObject.GetComponent<PlaneScrip>(); // speler
        if (projectile != null)
        {
            game.RemoveEnemy(gameObject); // verwijst naar de functie van gamemanager
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (player != null)
        {
            game.RemoveEnemy(gameObject);
            game.ReportPlayerHit();
        }
    }
}
