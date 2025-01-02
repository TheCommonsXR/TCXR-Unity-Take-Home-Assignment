using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;



public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBarFill;
    float maxHealth;
    float currentHealth;

    public Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        currentHealth = (float) health.currentHP;
        maxHealth = (float) health.maxHP;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = (float) health.currentHP;
    }
}
