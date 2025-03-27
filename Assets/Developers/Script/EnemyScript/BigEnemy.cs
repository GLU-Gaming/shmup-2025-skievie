using UnityEngine;

public class BigEnemy : EnemyScript
{
    public override void Activate()
    {
        HPamount = 9;
        game.AddScore(35);
        Destroy(gameObject);
    }
}
