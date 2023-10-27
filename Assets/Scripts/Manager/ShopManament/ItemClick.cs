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

    private void HideItem()
    {
        Tooltip.HideTooltip_Static();
    }

    private void OnItemClick()
    {
        MoneyManager.Instance.MinusMoney(item.price);
        if (MoneyManager.Instance.isEnough)
        {
            InventoryManager.Instance.addItem(item);
            InventoryManager.Instance.UpdateInventory();
            MoneyManager.Instance.SaveData();
            LoadShopItems.Instance.RenderState();
            Debug.Log($"Remain: {MoneyManager.Instance.TotalMoney}");
        }
        else if (MoneyManager.Instance.isEnough == false)
        {
            System.Func<string> getTooltipTextFunc = () =>
            {
                return "<color=#FF0000>Not enough money</color>";
            };
            Tooltip.ShowTooltip_Static(getTooltipTextFunc);
            Invoke("HideItem", 1.5f);
        }
    }
}
