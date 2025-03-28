using UnityEngine;

public class BigEnemy : EnemyScript
{
    //[SerializeField] private float moveSpeedBig = 8;
    //private Rigidbody rbBig;

    //private void Start()
    //{
    //    transform.rotation = Quaternion.Euler(-90, 0, 90);                                            
    //    rbBig.AddForce(new Vector3(-transform.position.x + -90, 0, 90) * moveSpeedBig, ForceMode.Acceleration);
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
