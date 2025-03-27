using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject EnemyTest;
    [SerializeField] private float moveSpeed = 8;

    public GameManagement game; // script aan script 

    [SerializeField] int scoreAmount;

    public int HPamount;

    public float fireRate = 0.5f;
    public float fireDamage = 17;

    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private GameObject EnemyBulletSpawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Acceleration); // movement van enemy

        game = FindAnyObjectByType<GameManagement>();
    }
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision) // collide om de enemy te verwijderen, geldt ook voor de kogel 
    {
        Shooter projectile = collision.gameObject.GetComponent<Shooter>(); // kogel 
        PlaneScrip player = collision.gameObject.GetComponent<PlaneScrip>(); // speler

        HPamount -= 1;

        if (projectile != null)
        {
            Destroy(collision.gameObject);

            if (HPamount == 0)
            {
                game.AddScore(scoreAmount);
                game.RemoveEnemy(gameObject); // verwijst naar de functie van gamemanager
            }
            else { }

        }

        if (player != null)
        {
            game.RemoveEnemy(gameObject);
            game.ReportPlayerHit();
            collision.transform.position = new Vector3(-6, 0, 12);
        }



    }

    public void EnemyHPdown()
    {
        if (HPamount == 0)
        {
            game.RemoveEnemy(gameObject);
        }


        HPamount -= 1;
    }

    public virtual void Activate()
    {

    }

    //public void FireEnemyBullet()
    //{
    //    if (laserKogel != null && bulletSpawnPointTest != null)
    //    {
    //        Instantiate(laserKogel, bulletSpawnPointTest.transform.position, laserKogel.transform.rotation);
    //        shootCooldownTimer = 1f;
    //    }
    //}

}
