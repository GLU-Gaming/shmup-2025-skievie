using UnityEngine;

public class SmallEnemy : EnemyScript
{
    //[SerializeField] private float moveSpeedSmall = 8;
    //private Rigidbody rbSmall;

    //private void Start()
    //{
    //    transform.rotation = Quaternion.Euler(-90, 180, 0);
    //    rbSmall.AddForce(new Vector3(-transform.position.x + -90, 0, 0) * moveSpeedSmall, ForceMode.Acceleration);
    //}
    public override void Activate()
    {
        game.TakeDamageFromEnemy(10);
        fireRate = 0.25f; // ingame veranderd
        HPamount = 3;
        game.AddScore(15);
        Destroy(gameObject);
    }
}
