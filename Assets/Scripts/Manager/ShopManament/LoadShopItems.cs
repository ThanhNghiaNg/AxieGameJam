using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadShopItems : MonoBehaviour
{
    public static LoadShopItems Instance { get; set; }
    public List<Item> items;
    private Transform container;
    private Transform shopItemTemplate;
    void Awake()
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
        container = GameObject.FindGameObjectWithTag("ContainerShopTemplate").transform;
        shopItemTemplate = container.Find("shopItemTemplate");
        RenderState();
    }
    void Start()
    {

    }
    public void OnShopItemClick()
    {

    }
    public void CreateItemButton(Item item, int itemCost, Sprite itemSprite, string itemName, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 68f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        Transform buttonItem = shopItemRectTransform.Find("ButtonItem");
        buttonItem.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        buttonItem.Find("ItemCost").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        buttonItem.GetComponent<ItemClick>().item = item;
        buttonItem.Find("ItemImage").GetComponent<Image>().sprite = itemSprite;
    }
    public void DisplayShopItems(Canvas canvas)
    {
        for (int i = 0; i < items.Count; i++)
        {
            CreateItemButton(items[i], items[i].price, items[i].sprite, items[i].itemName, i);
        }
    }

    public void RenderState()
    {
        Transform totalMoneyText = transform.Find("TotalMoneyText");
        totalMoneyText.GetComponent<TextMeshProUGUI>().SetText(MoneyManager.Instance.TotalMoney.ToString());
    }

}
