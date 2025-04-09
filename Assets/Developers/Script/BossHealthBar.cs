using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image HPbarBoss;

    public FinalBossScript BossScript;

    void Start()
    {
        currentHealth = maxHealth;
        //UpdateHealthBar();
    }

    //void UpdateHealthBar()
    //{
    //    HPbarBoss = currentHealth / maxHealth;
    //}

    //public void TakeDamage()
    //{
    //    BossScript.BossHPdown();
    //    UpdateHealthBar();
    //}

    //public void RestoreHealth(float amount)
    //{
    //    currentHealth += amount;
    //    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    //    UpdateHealthBar();
    //}

    void Update()
    {

    }
}

