using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image texture;
    [SerializeField] private TMP_Text quantityText;

    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void Setup(InventoryItemData itemData)
    {
        texture.sprite = itemData.itemIcon;
        quantityText.text = "1";
        gameObject.SetActive(true);
    }

    public void SetQuantity(int quantity)
    {
        quantityText.text = quantity.ToString();
    }
}
