using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int СurrentHealth { get; private set; }

    public event Action<float> OnHealthChanged;

    private void Awake()
    {
        СurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        СurrentHealth -= damage;

        OnHealthChanged?.Invoke(СurrentHealth);

        if (СurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
