using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemSlot[] items;
    private Dictionary<InventoryItemData, ItemSlot> currentItems = new();
    private Queue<ItemSlot> freeItemSlots;
    private InventorySystem inventory;

    private void Start()
    {
        inventory = InventorySystem.Instance;

        foreach (var item in items)
        {
            item.Reset();
        }

        freeItemSlots = new Queue<ItemSlot>(items);

        inventory.OnItemAdded += OnItemAdded;
        inventory.OnItemRemoved += OnItemRemoved;
        inventory.OnItemUpdated += OnItemUpdated;
    }

    private void OnItemUpdated(InventoryItemData itemData, int quantity)
    {
        if (currentItems.TryGetValue(itemData, out ItemSlot itemSlot))
        {
            itemSlot.SetQuantity(quantity);
        }
    }

    private void OnItemRemoved(InventoryItemData itemData)
    {
        if (currentItems.TryGetValue(itemData, out ItemSlot itemSlot))
        {
            itemSlot.Reset();
            freeItemSlots.Enqueue(itemSlot);
        }
    }

    private void OnItemAdded(InventoryItemData itemData)
    {
        if (currentItems.TryGetValue(itemData, out ItemSlot itemSlot))
        {
            
        }
        else
        {
            if (freeItemSlots.Count > 0)
            {
                var freeSlot = freeItemSlots.Dequeue();
                currentItems.Add(itemData, freeSlot);
                freeSlot.Setup(itemData);
            }
            else
            {
                Debug.LogError("No tenes lugar en el inventario de la UI");
            }
        }
    }
}
