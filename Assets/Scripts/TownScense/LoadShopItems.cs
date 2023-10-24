using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

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
    public void CreateItemButton(int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
    }
    public void DisplayShopItems(Canvas canvas)
    {
        Debug.Log("lmao");
        Debug.Log(container);
        Debug.Log(shopItemTemplate);
        CreateItemButton(0);
        CreateItemButton(1);
        CreateItemButton(2);
    }

}
