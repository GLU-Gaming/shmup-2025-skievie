using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private float EnemyAmount = 4;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private LayerMask enemyLayer;

    public float lifeAmount = 3;

    public PlaneScrip PlanePlayerScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartNewRound()
    {
        if (spawnedEnemies.Count < EnemyAmount)
        {
            Vector3 spawnpoint = new Vector3(34, Random.Range(-15, 15), 0);

            bool temp = AsteroidPlayerOverlap(spawnpoint, 1);
            if (temp)
            {
                GameObject go = Instantiate(GameObject[Enemies], spawnpoint, transform.rotation);
                spawnedEnemies.Add(go);
            }
        }
    }

    public void RemoveEnemy(GameObject enemiesToRemove) // verwijderen van enemy
    {
        
        spawnedEnemies.Remove(enemiesToRemove);
        Destroy(enemiesToRemove);

        if (spawnedEnemies.Count == 0)
        {
            StartNewRound();
        }

    }

    public bool AsteroidPlayerOverlap(Vector3 center, float radius)
    {
        bool freeSpace = false;

        Collider[] Asteroids = Physics.OverlapSphere(Vector3.zero, radius, enemyLayer.value);
        if (Asteroids.Length == 0)
        {
            freeSpace = true;
        }
        return freeSpace;



    }

    public void ReportPlayerHit()
    {


        if (lifeAmount > 2)
        {
            ResetPlayer();
            
        }
        else if (lifeAmount > 1)
        {
            
            ResetPlayer();
        }
        else if (lifeAmount == 1)
        {
            
            //SceneManager.LoadScene("GameOverScreen");
        }
        else
        {

            ResetPlayer();
            
        }

        lifeAmount -= 1;


    }

    private void ResetPlayer()
    {

        transform.position = Vector3.zero;
        PlaneScrip player = FindFirstObjectByType<PlaneScrip>();

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        player.transform.position = Vector3.zero;
        player.transform.eulerAngles = Vector3.left * 90;
    }

}
