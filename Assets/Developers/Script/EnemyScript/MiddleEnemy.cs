using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    public override void Activate()
    {
        EnemyHPdown(6);
        game.AddScore(25);
        Destroy(gameObject);
    }
}
