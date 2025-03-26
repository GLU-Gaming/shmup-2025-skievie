using UnityEngine;

public class BigEnemy : EnemyScript
{
    public override void Activate()
    {
        EnemyHPdown(12);
        game.AddScore(35);
        Destroy(gameObject);
    }
}
