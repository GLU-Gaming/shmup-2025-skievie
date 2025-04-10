using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Components")]
    [SerializeField] private Slider healthSlider;

    public delegate void PlayerDeathDelegate();
    public event PlayerDeathDelegate OnPlayerDeath;

    private void Awake()
    {
        if (healthSlider == null)
        {
            Transform sliderTransform = transform.Find("Canvas/HealthbarSlider");
            if (sliderTransform != null)
                healthSlider = sliderTransform.GetComponent<Slider>();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
            OnPlayerDeath?.Invoke(); 
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public float GetCurrentHealth() => currentHealth;
}
