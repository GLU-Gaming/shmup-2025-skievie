using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    public override void Activate()
    {
        HPamount = 6;
        EnemyHPdown();
        game.AddScore(25);
        Destroy(gameObject);
    }
}
