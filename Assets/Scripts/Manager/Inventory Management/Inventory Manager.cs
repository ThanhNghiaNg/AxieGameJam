using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<Item> Inventory;
    public int slotLimit = 10;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private bool IsExist(Item item)
    {
        foreach (Item inventoryItem in Inventory)
        {
            if (inventoryItem == null)
            {
                continue;
            }
            else
            {
                if (item.id == inventoryItem.id)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void addItem(Item item)
    {
        if (IsExist(item))
        {
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity++;
        }
        else
        {
            Inventory.Add(item);
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity = 1;
        }
    }

    public void removeItem(Item item)
    {
        if (IsExist(item) && item.quatity > 1)
        {
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity--;
        }
        else if (IsExist(item) && item.quatity == 1)
        {
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity = 0;
            Inventory.Remove(item);
        }
    }
}
