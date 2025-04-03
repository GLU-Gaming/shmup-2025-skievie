using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameManagement gameManager;

    private void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManagement>();
        }

        
        slider.maxValue = gameManager.playerHP;
        slider.value = gameManager.playerHP;
    }

    private void Update()
    {
        
        slider.value = gameManager.playerHP;
    }
}