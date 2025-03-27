using UnityEngine;

public class SmallEnemy : EnemyScript
{
    public override void Activate()
    {
        //fireDamage = 10;
        game.TakeDamageFromEnemy(10);
        fireRate = 0.25f;
        HPamount = 3;
        game.AddScore(15);
        Destroy(gameObject);
    }
}
