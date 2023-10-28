using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<Item> Inventory;
    public List<Transform> inventoryTF;
    public int slotLimit = 10;
    public Transform container;
    public Transform ItemCard;
    private float startX = -270f;
    private float startY = 55f;
    private string dataFolderPath;
    private string dataFilePath;

    private void Awake()
    {
        dataFolderPath = Path.Combine(Application.dataPath, "Data");
        string dataFileName = "InventoryData.json";
        dataFilePath = Path.Combine(dataFolderPath, dataFileName);

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        ItemCard.gameObject.SetActive(false);
        LoadData();
        SaveData();
    }

    private void Start()
    {
        int index = 0;
        foreach (Item item in Inventory)
        {
            if (item == null)
            {
                CreateCardButton(null, null, index);
            }
            else
            {
                int itemIndex = Inventory.IndexOf(item);
                CreateCardButton(Inventory[itemIndex].sprite, Inventory[itemIndex].quatity, index);
            }
            index++;

        }
    }
    public void SaveData()
    {
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
        Dictionary<string, object> data = new Dictionary<string, object>();

        string[] itemFilePaths = new string[Inventory.Count];
        for (int i = 0; i < Inventory.Count; i++)
        {
            itemFilePaths[i] = AssetDatabase.GetAssetPath(Inventory[i]);
        }

        data.Add("items", itemFilePaths);

        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(dataFilePath, json);
        Debug.Log("Data saved!");
    }
    public void LoadData()
    {
        if (File.Exists(dataFilePath))
        {
            string json = File.ReadAllText(dataFilePath);
            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            string[] itemFilePaths = ((Newtonsoft.Json.Linq.JArray)data["items"]).ToObject<string[]>();
            Debug.Log($"itemFilePaths: {JsonConvert.SerializeObject(itemFilePaths)}");
            for (int i = 0; i < itemFilePaths.Length; i++)
            {
                if (Inventory.Count < i + 1)
                {
                    Inventory.Add(AssetDatabase.LoadAssetAtPath<Item>(itemFilePaths[i]));
                }
                else
                {
                    Inventory[i] = AssetDatabase.LoadAssetAtPath<Item>(itemFilePaths[i]);
                }
            }
        }
        else
        {
            Debug.LogError("JSON file not found.");
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
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i] == null)
                {
                    Inventory[i] = item;
                    Inventory[i].quatity = 1;
                    break;
                }
            }
        }
        SaveData();
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
            Inventory[index] = null;
        }
    }

    private void CreateCardButton(Sprite itemSprite, int? quatity, int index)
    {
        Transform axieCardTransform = Instantiate(ItemCard, container);
        RectTransform axieCardRectTransform = axieCardTransform.GetComponent<RectTransform>();
        if (index <= 4)
        {
            axieCardRectTransform.anchoredPosition = new Vector2(startX, startY);
            startX += 130;
            if (index == 4)
            {
                startX = -270f;
            }
        }
        else if (index >= 5)
        {
            startY = -80f;
            axieCardRectTransform.anchoredPosition = new Vector2(startX, startY);
            startX += 130;
        }


        if (itemSprite != null)
        {
            axieCardTransform.Find("Image").GetComponent<Image>().sprite = itemSprite;
            axieCardTransform.Find("quatity").GetComponent<TextMeshProUGUI>().SetText(quatity.ToString());
        }
        else
        {
            axieCardTransform.Find("Image").GetComponent<Image>().sprite = null;
            axieCardTransform.Find("Image").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            axieCardTransform.Find("quatity").GetComponent<TextMeshProUGUI>().SetText("");
        }
        inventoryTF.Add(axieCardTransform);
        axieCardTransform.gameObject.SetActive(true);
    }

    public void UpdateInventory()
    {
        int index = 0;
        foreach (Item item in Inventory)
        {
            if (item == null)
            {
                continue;
            }
            else
            {
                int itemIndex = Inventory.IndexOf(item);
                inventoryTF[itemIndex].transform.Find("Image").GetComponent<Image>().sprite = item.sprite;
                inventoryTF[itemIndex].transform.Find("Image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                inventoryTF[itemIndex].transform.Find("quatity").GetComponent<TextMeshProUGUI>().SetText(item.quatity.ToString());
            }
            index++;
        }
    }

}
