using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image HPBarBoss;

    public FinalBossScript BossScript;

    public GameObject BossHealthBarUI;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        BossScript = FindAnyObjectByType<FinalBossScript>();
    }

    void UpdateHealthBar()
    {
        float currentHealth = BossScript.HPamount;
        HPBarBoss.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakenDamage()
    {
        BossScript.TakeDamage(1);
        BossScript.BossHPdown();
        UpdateHealthBar();
    }

    void Update()
    {
        if (BossScript != null)
        {
            UpdateHealthBar();
        }
        else if (BossScript == null)
        {
            BossHealthBarUI.SetActive(false);
        }
    }
}

