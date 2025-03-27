using UnityEngine;

public class SmallEnemy : EnemyScript
{
    public override void Activate()
    {
        HPamount = 3;
        EnemyHPdown();
        game.AddScore(15);
        Destroy(gameObject);
    }
}
