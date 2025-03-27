using UnityEngine;

public class SmallEnemy : EnemyScript
{
    public override void Activate()
    {
        game.TakeDamageFromEnemy(10);
        fireRateTimer = 0.25f;
        HPamount = 3;
        game.AddScore(15);
        Destroy(gameObject);
    }
}
