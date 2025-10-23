using UnityEngine;
using System;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float visionRange = 5f;
    [SerializeField] private LayerMask playerLayer;

    public bool CanSeePlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, visionRange, playerLayer);
        if (player == null) return false;

        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true; // Player is visible
        }
            
        return false; // No visible player found
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
