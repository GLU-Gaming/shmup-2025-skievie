using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    public override void Activate()
    {
        game.TakeDamageFromEnemy(17);
        fireRate = 0.5f; // ingame veranderd
        HPamount = 6;
        game.AddScore(25);
        Destroy(gameObject);
    }
}
