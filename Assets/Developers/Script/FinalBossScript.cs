using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossScript : MonoBehaviour
{
    [SerializeField] public Transform firePointHigh;
    [SerializeField] public Transform firePointLow;
    [SerializeField] public GameObject bulletPrefab;

    public Rigidbody rb;
    public GameManagement game;
    public Transform player;
    private float nextFireTime;
    public float fireRate;
    public float fireRateTimer = 0.5f;
    public float fireDamage = 17;

    [SerializeField] private GameObject EnemyBullet;


    public int HPamount; // boss HP = 100
    [SerializeField] int scoreAmount; // score amount = 1000

    void Start()
    {
        rb = GetComponent<Rigidbody>();

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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            if (bullet != null)
            {
                BossHPdown();
                Destroy(collision.gameObject);
            }
        }
    }

    public void BossHPdown() // spreekt voorzich
    {
        HPamount -= 1;
        if (HPamount == 0)
        {
            game.AddScore(scoreAmount);
            Destroy(gameObject);
            DefeatBossToWinScreen();
        }
    }

    public void FireEnemyBullet() // fire bullet
    {
        if (EnemyBullet != null && firePointHigh != null && firePointLow != null)
        {
            Instantiate(EnemyBullet, firePointHigh.transform.position, EnemyBullet.transform.rotation);
            Instantiate(EnemyBullet, firePointLow.transform.position, EnemyBullet.transform.rotation);
        }
    }

    public void DefeatBossToWinScreen()
    {
        Invoke(nameof(Destroy), 6f);
        SceneManager.LoadScene("EndGameScreen");
    }

    public void TakeDamage(int damage)
    {
        HPamount -= damage;
        if (HPamount <= 0)
        {
            if (game != null)
                game.EnemyDied(gameObject, scoreAmount);
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        if (game != null)
            game.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    
}
