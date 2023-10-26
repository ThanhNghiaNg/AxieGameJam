using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    public Button yourButton;
    public Item item;
    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(OnItemClick);
    }

    private void OnItemClick()
    {
        MoneyManager.Instance.MinusMoney(item.price);
        if (MoneyManager.Instance.isEnough)
        {
            InventoryManager.Instance.addItem(item);
            // InventoryManager.Instance.UpdateInventory();
            MoneyManager.Instance.SaveData();
            LoadShopItems.Instance.RenderState();
            Debug.Log($"Remain: {MoneyManager.Instance.TotalMoney}");
        }
    }
}
