using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public event Action<InventoryItemData> OnItemAdded;
    public event Action<InventoryItemData, int> OnItemUpdated;
    public event Action<InventoryItemData> OnItemRemoved;
    
    public static InventorySystem Instance;
    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        Instance = this;
    }

    public void Add(InventoryItemData itemData)
    {
        if (_itemDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            Debug.Log("Sumar stack en item");
            value.AddStack();
            OnItemUpdated?.Invoke(itemData, value.stackSize);
        }
        else
        {
            Debug.Log("Agregar un nuevo item");
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            _itemDictionary.Add(itemData,newItem);
            OnItemAdded?.Invoke(itemData);
        }
        
    }

    public void Remove(InventoryItemData itemData)
    {
        if (_itemDictionary.TryGetValue(itemData, out  InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize==0)
            {
                inventory.Remove(value);
                _itemDictionary.Remove(itemData);
                OnItemRemoved?.Invoke(itemData);
            }
        }
        
    }
}
