using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    public override void Activate()
    {
        EnemyHPdown(8);
        game.AddScore(25);
        Destroy(gameObject);
    }
}
