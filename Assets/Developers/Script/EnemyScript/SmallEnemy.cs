using UnityEngine;

public class SmallEnemy : EnemyScript
{
    public override void Activate()
    {
        game.TakeDamageFromEnemy(10);
        fireRate = 0.25f; // ingame veranderd
        HPamount = 3;
        game.AddScore(15);
        Destroy(gameObject);
    }
}
