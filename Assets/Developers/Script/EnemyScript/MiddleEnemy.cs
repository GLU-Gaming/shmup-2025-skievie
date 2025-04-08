using UnityEngine;

public class MiddleEnemy : EnemyScript
{
    public override void Activate()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        base.Activate();
        fireRate = 0.75f;
        HPamount = 4;
        scoreAmount = 35;
    }
}