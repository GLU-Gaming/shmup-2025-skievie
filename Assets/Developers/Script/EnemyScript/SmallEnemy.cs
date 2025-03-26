using UnityEngine;

public class SmallEnemy : EnemyScript
{
    public override void Activate()
    {
        EnemyHPdown(3);
        game.AddScore(15);
        Destroy(gameObject);
    }
}
