using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
[System.Serializable]
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int TotalMoney;
    public bool isEnough;
    private string dataFolderPath;
    private string dataFilePath;

    private void Awake()
    {
        dataFolderPath = Path.Combine(Application.dataPath, "Data");
        string dataFileName = "MoneyData.json";
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
        LoadData();
        SaveData();
    }

    public void LoadData()
    {
        if (File.Exists(dataFilePath))
        {
            string json = File.ReadAllText(dataFilePath);
            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (data.ContainsKey("TotalMoney")) TotalMoney = System.Convert.ToInt32(data["TotalMoney"]);
        }
        else
        {
            TotalMoney = 0;
            Debug.LogError("JSON file not found.");
        }
    }
    public void SaveData()
    {
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("TotalMoney", TotalMoney);
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(dataFilePath, json);
    }

    public void AddMoney(int money)
    {
        TotalMoney += money;
    }

    public void MinusMoney(int money)
    {
        if (money <= TotalMoney)
        {
            isEnough = true;
            TotalMoney -= money;
        }
        else if (money > TotalMoney)
        {
            isEnough = false;
        }
    }
}
