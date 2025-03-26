using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private float EnemyAmount = 3;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private LayerMask enemyLayer;

    public float lifeAmount = 3;

    public PlaneScrip PlanePlayerScript;

    [SerializeField] private TextMeshProUGUI scoreText;

    public int score;
    public int highScore;

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
        while (spawnedEnemies.Count < EnemyAmount) // blijven spawnen
        {
            SpawnEnemy();
        }
    }


    public void EnemyDied(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy); // remove hen
        Destroy(enemy); // verwijder

        SpawnEnemy(); // Spawn een enemy
    }

    private void SpawnEnemy()
    {
        Vector3 spawnpoint = new Vector3(Random.Range(18, 26), Random.Range(-6, 6), 12);

        if (EnemyPlayerOverlap(spawnpoint, 1))
        {
            GameObject go = Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnpoint, transform.rotation);
            spawnedEnemies.Add(go);
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

    public bool EnemyPlayerOverlap(Vector3 center, float radius)
    {
        bool freeSpace = false;

        Collider[] Enemies = Physics.OverlapSphere(Vector3.zero, radius, enemyLayer.value);
        if (Enemies.Length == 0)
        {
            freeSpace = true;
        }
        return freeSpace;



    }

    public void ReportPlayerHit()
    {


       if (lifeAmount == 1)
        {
            
            SceneManager.LoadScene("EndGameScreen");
        }
        else

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

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }


}
