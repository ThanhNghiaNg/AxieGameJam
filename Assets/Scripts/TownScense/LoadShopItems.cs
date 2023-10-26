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
    }
    void Start()
    {

    }
    public void CreateItemButton(int itemCost, Sprite itemSprite, string itemName, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 68f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        shopItemRectTransform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemRectTransform.Find("ItemCost").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemRectTransform.Find("ItemImage").GetComponent<Image>().sprite = itemSprite;
    }
    public void DisplayShopItems(Canvas canvas)
    {
        Debug.Log("lmao");
        Debug.Log(container);
        Debug.Log(shopItemTemplate);
        Debug.Log($"count: {items.Count}");
        for (int i = 0; i < items.Count; i++)
        {
            CreateItemButton(items[i].price, items[i].sprite, items[i].itemName, i);
        }

    }

}
