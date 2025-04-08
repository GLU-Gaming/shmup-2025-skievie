using UnityEngine;

public class SmallEnemy : EnemyScript
{
    public override void Activate()
    {
        transform.rotation = Quaternion.Euler(-90, 180, 0);
        base.Activate();
        fireRate = 0.75f;
        HPamount = 9;
        scoreAmount = 35; 
    }
}