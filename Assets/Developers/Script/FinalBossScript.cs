using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossScript : MonoBehaviour
{
    [SerializeField] public Transform firePointHigh;
    [SerializeField] public Transform firePointLow;
    [SerializeField] public GameObject bulletPrefab;

    private Rigidbody rb;
    public GameManagement game;
    public Transform player;
    private float nextFireTime;
    public float fireRate;
    public float fireRateTimer = 0.5f;
    public float fireDamage = 17;

    [SerializeField] private GameObject EnemyBullet;


    public int HPamount; // boss HP = 100
    [SerializeField] int scoreAmount; // score amount = 1000

    public BossHealthBar bossHealthBar;



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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletScript bullet = other.GetComponent<BulletScript>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
        }
    }

    public void BossHPdown() // spreekt voorzich
    {
        HPamount -= 1;
        if (HPamount == 0)
        {
            game.AddScore(scoreAmount);
            Destroy(bossHealthBar.BossHealthBarUI);
            Destroy(gameObject);
            game.DefeatBossToWinScreen();
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

    public void TakeDamage(int damage)
    {
        HPamount -= damage;
        if (HPamount <= 0)
        {
            if (game != null)
                game.BossDied(gameObject, scoreAmount);
            DestroyBoss();
        }
    }

    private void DestroyBoss()
    {
        if (game != null)
            game.RemoveBoss(gameObject);
        bossHealthBar.BossHealthBarUI.SetActive(false);
        Destroy(gameObject);
    }

    
}
