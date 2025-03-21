using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] private GameObject Enemies;
    [SerializeField] private float EnemyAmount = 4;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveEnemy(GameObject enemiesToRemove)
    {
        
        spawnedEnemies.Remove(enemiesToRemove);
        Destroy(enemiesToRemove);

        

    }

}
