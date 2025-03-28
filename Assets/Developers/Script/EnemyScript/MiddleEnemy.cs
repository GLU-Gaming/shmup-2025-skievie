using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    //[SerializeField] private float moveSpeed = 8;
    //private Rigidbody rbMiddle;

    //private void Start()
    //{
    //    rbMiddle.AddForce(new Vector3(-transform.position.x, 0, 0) * moveSpeed, ForceMode.Acceleration);
    //}
    public override void Activate()
    {
        game.TakeDamageFromEnemy(17);
        fireRate = 0.5f; // ingame veranderd
        HPamount = 6;
        game.AddScore(25);
        Destroy(gameObject);
    }
}
