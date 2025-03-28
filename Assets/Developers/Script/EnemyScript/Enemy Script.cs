using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject EnemyTest;
    //[SerializeField] private float moveSpeed = 8;

    public GameManagement game; // script aan script 

    [SerializeField] int scoreAmount;

    public int HPamount; // enemy HP

    public float fireRate;
    public float fireRateTimer = 0.5f;
    public float fireDamage = 17;

    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private GameObject EnemyBulletSpawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Acceleration); // movement van enemy

        game = FindAnyObjectByType<GameManagement>();

        
    }
    void Update()
    {
        if (fireRateTimer <= 0)
        {
            FireEnemyBullet();
            fireRateTimer = fireRate;

        }
        else if (fireRateTimer > 0) 
        {
            fireRateTimer -= Time.deltaTime;
        }
        
    }

   
    public void OnCollisionEnter(Collision collision) // collide om de enemy te verwijderen, geldt ook voor de kogel 
    {
        if (collision.gameObject.CompareTag("Bullet") == true)
        {
            EnemyHPdown();
            Destroy(collision.gameObject);

        }
    }

    public void EnemyHPdown()
    {
        HPamount -= 1;
        if (HPamount == 0)
        {
            game.AddScore(scoreAmount);
            game.RemoveEnemy(gameObject);
        }
    }

    public virtual void Activate()
    {

    }

    public void FireEnemyBullet()
    {
        if (EnemyBullet != null && EnemyBulletSpawnPoint != null)
        {
            Instantiate(EnemyBullet, EnemyBulletSpawnPoint.transform.position, EnemyBullet.transform.rotation);
        }
    }

}
