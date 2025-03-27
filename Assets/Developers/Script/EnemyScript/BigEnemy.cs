using UnityEngine;

public class BigEnemy : EnemyScript
{
    public override void Activate()
    {
        game.TakeDamageFromEnemy(25);
        fireRate = 0.75f; // ingame veranderd
        HPamount = 9;
        game.AddScore(35);
        Destroy(gameObject);
    }
}
