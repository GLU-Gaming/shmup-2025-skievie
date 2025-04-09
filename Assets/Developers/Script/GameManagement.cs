using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManagement : MonoBehaviour
{
    public enum EnemyType { Small, Medium, Big }
    public enum GamePhase { Preparation, Combat, Cooldown }

    [System.Serializable]
    public class EnemyPrefab
    {
        public EnemyType type;
        public EnemyScript prefab;
    }

    [System.Serializable]
    public class EnemyWave
    {
        public EnemyType[] enemiesInWave;
        public float spawnInterval = 0.5f;
        public float waveDuration = 5f;
    }

    [Header("Enemy Settings")]
    [SerializeField] public EnemyPrefab[] enemyPrefabs; // Added enemyPrefabs array
    [SerializeField] public EnemyWave[] waves;
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] private float spawnRadius = 3f;

    [Header("Player Settings")]
    [SerializeField] private PlaneScript playerScript;
    public float maxPlayerHP = 100f;
    public float playerHP = 100f;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject gameOverScreen;

    // Game state
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int currentWaveIndex = 0;
    private GamePhase currentPhase = GamePhase.Preparation;
    public int score;
    public int highScore;

    public AudioSource audioEnemyDeath;
    

    private void Start()
    {
        StartCoroutine(GameLoop());
        LoadHighscore();
    }

    private IEnumerator GameLoop()
    {
        while (playerHP > 0)
        {
            // Preparation phase
            currentPhase = GamePhase.Preparation;
            UpdateWaveUI();
            yield return new WaitForSeconds(2f); // Brief pause between waves

            // Combat phase
            currentPhase = GamePhase.Combat;
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));

            // Cooldown phase (despawn remaining enemies)
            currentPhase = GamePhase.Cooldown;
            yield return StartCoroutine(CleanupWave());

            // Advance to next wave
            currentWaveIndex = (currentWaveIndex + 1) % waves.Length;
        }

        GameOver();
    }

    private IEnumerator SpawnWave(EnemyWave wave)
    {
        float waveTimer = 0f;
        int spawnIndex = 0;

        while (waveTimer < wave.waveDuration && spawnIndex < wave.enemiesInWave.Length)
        {
            SpawnEnemy(wave.enemiesInWave[spawnIndex]);
            spawnIndex++;
            waveTimer += wave.spawnInterval;
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    private IEnumerator CleanupWave()
    {
        // Despawn all enemies after 5 seconds
        yield return new WaitForSeconds(5f);

        foreach (var enemy in activeEnemies.ToArray())
        {
            if (enemy != null)
            {
                RemoveEnemy(enemy);
            }
        }
    }

    private void SpawnEnemy(EnemyType type)
    {
        Vector3 spawnPos = GetValidSpawnPosition();
        var enemyPrefab = System.Array.Find(enemyPrefabs, x => x.type == type);

        if (enemyPrefab != null && spawnPos != Vector3.zero)
        {
            EnemyScript enemy = Instantiate(
                enemyPrefab.prefab,
                spawnPos,
                Quaternion.identity
            );

            activeEnemies.Add(enemy.gameObject);
            enemy.Activate();

            // Auto-remove after 5 seconds if not destroyed
            StartCoroutine(AutoRemoveEnemy(enemy.gameObject, 5f));
        }
    }

    private IEnumerator AutoRemoveEnemy(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enemy != null) RemoveEnemy(enemy);
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(18, 26),
                Random.Range(-5, 5),
                12
            );

            if (Physics.OverlapSphere(randomPos, spawnRadius, enemyLayer).Length == 0)
            {
                return randomPos;
            }
        }
        return Vector3.zero;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }

    public void EnemyDied(GameObject enemy, int scoreValue)
    {
        audioEnemyDeath.Play();
        AddScore(scoreValue);
        RemoveEnemy(enemy);
    }

    public void ReportPlayerHit(float damage)
    {
        playerHP -= damage;
        playerHP = Mathf.Clamp(playerHP, 0, maxPlayerHP);

        if (playerHP <= 0) GameOver();
    }

    private void GameOver()
    {
        StopAllCoroutines();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave {currentWaveIndex + 1}\nPhase: {currentPhase}";
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}\nHigh Score: {highScore}";
        }
    }

    private void SaveHighScore() => PlayerPrefs.SetInt("HighScore", highScore);
    private void LoadHighscore() => highScore = PlayerPrefs.GetInt("HighScore", 0);

    public void RemoveBoss(GameObject Boss)
    {
        Destroy(Boss);
    }

    public void BossDied(GameObject Boss, int scoreValue)
    {
        AddScore(scoreValue);
        RemoveBoss(Boss);
    }


}
