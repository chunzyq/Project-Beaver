using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    [Inject] private PlayerStats playerStats;
    [SerializeField] private TextMeshProUGUI hpAmount;

    void Awake()
    {
        playerStats.OnHealthChanged += UpdateHPUI;
    }

    private void Start()
    {
        UpdateHPUI(playerStats.СurrentHealth);
    }

    private void UpdateHPUI(float newHealth)
    {
        hpAmount.text = "HP: " + newHealth.ToString();
    }
}
