using UnityEngine;

public class SmallEnemy : EnemyScript
{
    public override void Activate()
    {
        base.Activate();
        fireRate = 0.75f;
        HPamount = 9;
        scoreAmount = 35; 
    }
}