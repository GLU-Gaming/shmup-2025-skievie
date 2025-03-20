using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject EnemyTest;
    [SerializeField] private float moveSpeed = 8;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Force); // movement van enemy
    }
    void Update()
    {
        
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    Projectile projectile = collision.gameObject.GetComponent<Projectile>();
    //    SpaceShip player = collision.gameObject.GetComponent<SpaceShip>();
    //    if (projectile != null)
    //    {

    //        game.AddScore(scoreAmount);
    //        Instantiate(Particle, transform.position, Quaternion.identity);
    //        game.RemoveAsteroid(gameObject);
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }

    //    if (player != null)
    //    {
    //        // hit Player
    //    }
    //}
}
