using System.Collections;
using UnityEngine;

public class EnemyHitPlayer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageInterval = 1f;

    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(DamageLoop(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DamageLoop(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        while (true)
        {
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
                Debug.Log("Player took " + damage + " damage from enemy.");
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
