using UnityEngine;

public class BigEnemy : EnemyScript
{
    public override void Activate()
    {
        game.TakeDamageFromEnemy(25);
        fireRateTimer = 0.75f;
        HPamount = 9;
        game.AddScore(35);
        Destroy(gameObject);
    }
}
