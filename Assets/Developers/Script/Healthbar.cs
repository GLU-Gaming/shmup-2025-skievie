using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color lowHealthColor = Color.red;

    private GameManagement gameManager;

    private void Start()
    {
        // Auto-get references if not set
        if (slider == null) slider = GetComponent<Slider>();
        if (gameManager == null) gameManager = FindObjectOfType<GameManagement>();

        // Initialize health bar
        slider.maxValue = gameManager.maxPlayerHP;
        slider.value = gameManager.playerHP;
    }

    private void Update()
    {
        // Update health value
        slider.value = gameManager.playerHP;

        // Change color based on health percentage
        float healthPercent = slider.value / slider.maxValue;
        fillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercent);
    }
}