using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

//[System.Serializable]
//public class WaveData
//{
//    public GameObject[] shipsToSpawn;
//}

public class GameManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private float EnemyAmount = 3;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private LayerMask enemyLayer;

    public float lifeAmount = 3;
    public float playerHP = 100;

    public PlaneScript PlanePlayerScript;
    public EnemyScript ScriptForEnemy;

    [SerializeField] private TextMeshProUGUI scoreText;

    public int score;
    public int highScore;

    //[SerializeField] WaveData[] waves;
    //int currentWave;
    void Start()
    {
        StartNewRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartNewRound()
    {

        int maxTries = 10; 
        int tries = 0;

        while (spawnedEnemies.Count < EnemyAmount && tries < maxTries)
        {
            SpawnEnemy();
            tries++;
        }

        if (spawnedEnemies.Count < EnemyAmount)
        {
            Invoke(nameof(StartNewRound), 1f); // probeer opnieuw na 1 seconde
        }
    }


    public void EnemyDied(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        Destroy(enemy);

        StartNewRound(); 
    }


    private void SpawnEnemy()
    {
        int maxAttempts = 10; 
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 spawnpoint = new Vector3(Random.Range(18, 26), Random.Range(-5, 5), 12);

            if (EnemyPlayerOverlap(spawnpoint, 3))
            {
                int RandomEnemy = Random.Range(0, Enemies.Length);
                GameObject go = Instantiate(Enemies[RandomEnemy], spawnpoint, Enemies[RandomEnemy].transform.rotation);
                spawnedEnemies.Add(go);
              
                return;
            }

            attempts++;
        }

        Debug.LogWarning("Failed to spawn an enemy after " + maxAttempts + " attempts.");
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

    public bool EnemyPlayerOverlap(Vector3 center, float radius)
    {
        bool freeSpace = false;

        Collider[] Enemies = Physics.OverlapSphere(center, radius, enemyLayer.value);
        if (Enemies.Length == 0)
        {
            freeSpace = true;
        }
        return freeSpace;



    }

    public void ReportPlayerHit()
    {

        
       if (lifeAmount == 0)
       {
            
            SceneManager.LoadScene("EndGameScreen");
       }
        

        lifeAmount -= 1;


    }



    public void AddScore(int amount)
    {
        score = score + (amount);

        scoreText.text = "Score: " + score;

        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }

        if (score == 1000)
        {
            SceneManager.LoadScene("Bossfightscene");
            LoadScore();
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("myScore", score);
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("myHighScore", highScore);
    }

    public void LoadScore()
    {
        int loadedNumber = PlayerPrefs.GetInt("myScore");
    }

    public void LoadHighscore()
    {
        int loadedNumber = PlayerPrefs.GetInt("myHighScore");
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void TakeDamageFromEnemy(int damage)
    {
        playerHP -= damage;

        if (playerHP <= 0)
        {
            lifeAmount -= 1;
        }
    }

}
