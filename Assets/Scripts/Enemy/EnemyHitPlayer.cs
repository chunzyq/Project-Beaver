using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHitPlayer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private float nextDamageTime = 0f;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
