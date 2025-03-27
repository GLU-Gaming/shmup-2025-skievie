using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    public override void Activate()
    {
        game.TakeDamageFromEnemy(17);
        fireRateTimer = 0.5f;
        HPamount = 6;
        game.AddScore(25);
        Destroy(gameObject);
    }
}
