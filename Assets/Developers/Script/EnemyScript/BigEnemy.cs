using UnityEngine;

public class BigEnemy : EnemyScript
{
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    public override void Activate()
    {
        game.TakeDamageFromEnemy(25);
        fireRate = 0.75f; 
        HPamount = 9;
        game.AddScore(35);
        Destroy(gameObject);

        
    }
}
