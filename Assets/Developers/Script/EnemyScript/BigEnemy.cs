using UnityEngine;

public class BigEnemy : EnemyScript
{
    //[SerializeField] private float moveSpeed = 8;
    //private Rigidbody rbBig;

    //private void Start()
    //{
    //    rbBig.AddForce(new Vector3(-transform.position.x + -90, 0, 90) * moveSpeed, ForceMode.Acceleration);
    //}
    public override void Activate()
    {
        game.TakeDamageFromEnemy(25);
        fireRate = 0.75f; // ingame veranderd
        HPamount = 9;
        game.AddScore(35);
        Destroy(gameObject);

        
    }
}
