using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemEffectSystem : MonoBehaviour
{
    [Inject] private Inventory inventory;
    [Inject] private PlayerStats playerStats;

    void OnEnable()
    {
        inventory.OnItemAdded += ApplyItemEffect;
    }
    void OnDisable()
    {
        inventory.OnItemAdded -= ApplyItemEffect;
    }

    private void ApplyItemEffect(Item item)
    {
        playerStats.ModifyHealth(item.damageToPlayer);
    }
}
