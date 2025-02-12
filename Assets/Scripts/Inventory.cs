using System.Collections.Generic; // Добавьте эту строку
using UnityEngine;

[System.Serializable]

public class Inventory
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(string itemName)
    {
        items.RemoveAll(item => item.itemName == itemName);
    }

    public void PrintInventory()
    {
        foreach (var item in items)
        {
            Debug.Log($"Item: {item.itemName}, Type: {item.Type}, Description: {item.Description}, Debuffs: {item.Debuffs}, ImagePath: {item.ImagePath}");
        }
    }
}