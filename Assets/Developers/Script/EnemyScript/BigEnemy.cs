using UnityEngine;

public class BigEnemy : EnemyScript
{
    public override void Activate()
    {
        //fireDamage = 25;
        game.TakeDamageFromEnemy(25);
        fireRate = 0.75f;
        HPamount = 9;
        game.AddScore(35);
        Destroy(gameObject);
    }
}
