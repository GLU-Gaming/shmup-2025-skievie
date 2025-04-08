using UnityEngine;

public class BigEnemy : EnemyScript
{
    public override void Activate()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 90);
        base.Activate();
        fireRate = 0.75f;
        HPamount = 6;
        scoreAmount = 35;
    }
}