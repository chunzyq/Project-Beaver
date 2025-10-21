using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventory = new List<Item>();

    public event Action<Item> OnItemAdded;

    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log("Item added to inventory: " + item.itemName);
        OnItemAdded?.Invoke(item);
    }
}
