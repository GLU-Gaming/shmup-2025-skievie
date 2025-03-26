using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject EnemyTest;
    [SerializeField] private float moveSpeed = 8;

    private float lessHP = 1;

    public GameManagement game; // script aan script 

    [SerializeField] int scoreAmount;

    [SerializeField] private EnemyScript enemyScript;

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
            game.AddScore(scoreAmount);
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

    public void EnemyHPdown(int HPamount)
    {
        if (HPamount == 0)
        {
            Destroy(gameObject);
        }


        lessHP -= 1;
    }

    public virtual void Activate()
    {
        
    }

}
