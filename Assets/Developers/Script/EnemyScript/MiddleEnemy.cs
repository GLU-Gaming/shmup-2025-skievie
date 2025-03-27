using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    public override void Activate()
    {
        //fireDamage = 17;
        game.TakeDamageFromEnemy(17);
        fireRate = 0.5f;
        HPamount = 6;
        game.AddScore(25);
        Destroy(gameObject);
    }
}
