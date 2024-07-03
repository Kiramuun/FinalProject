using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpManager : MonoBehaviour
{
    public static event Action<EnemyHpManager> OnEnemyKilled;
    [SerializeField] float health = 50;
    [SerializeField] float maxHealth = 50;
    [SerializeField] HealthBarManager healthBarManager;

    private void Awake()
    {
        healthBarManager = GetComponentInChildren<HealthBarManager>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBarManager.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBarManager.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("je suis mort");
    }
}
