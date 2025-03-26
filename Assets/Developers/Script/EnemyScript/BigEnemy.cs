using UnityEngine;

public class BigEnemy : EnemyScript
{
    public override void Activate()
    {
        EnemyHPdown(9);
        game.AddScore(35);
        Destroy(gameObject);
    }
}
