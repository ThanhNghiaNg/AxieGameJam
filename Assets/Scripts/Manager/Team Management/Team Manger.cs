using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.IO;
using Newtonsoft.Json;

public class TeamManager : MonoBehaviour
{
    static public TeamManager Instance { get; private set; }

    public List<Character> ownedAxie;

    [SerializeField]
    public List<Character> teamAxie;
    public Button[] buttonSlot;
    public Character currentSelectedAxie;
    public int currentSelectedSlot;
    private string dataFolderPath;
    private string dataFilePath;

    private void Awake()
    {
        dataFolderPath = Path.Combine(Application.dataPath, "Data");
        string dataFileName = "TeamData.json";
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

    public void SaveData()
    {
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
        Dictionary<string, object> data = new Dictionary<string, object>();

        string[] ownedAxieFilePaths = new string[ownedAxie.Count];
        for (int i = 0; i < ownedAxie.Count; i++)
        {
            ownedAxieFilePaths[i] = AssetDatabase.GetAssetPath(ownedAxie[i]);
        }

        string[] teamAxieFilePaths = new string[teamAxie.Count];
        for (int i = 0; i < teamAxie.Count; i++)
        {
            teamAxieFilePaths[i] = AssetDatabase.GetAssetPath(teamAxie[i]);
        }

        data.Add("ownedAxie", ownedAxieFilePaths);
        data.Add("teamAxie", teamAxieFilePaths);

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

            string[] ownedAxiePaths = ((Newtonsoft.Json.Linq.JArray)data["ownedAxie"]).ToObject<string[]>();
            for (int i = 0; i < ownedAxiePaths.Length; i++)
            {
                if (ownedAxie.Count < i + 1)
                {
                    ownedAxie.Add(AssetDatabase.LoadAssetAtPath<Character>(ownedAxiePaths[i]));
                }
                else
                {
                    ownedAxie[i] = AssetDatabase.LoadAssetAtPath<Character>(ownedAxiePaths[i]);
                }
            }

            string[] teamAxiePaths = ((Newtonsoft.Json.Linq.JArray)data["teamAxie"]).ToObject<string[]>();
            for (int i = 0; i < teamAxiePaths.Length; i++)
            {
                if (teamAxie.Count < i + 1)
                {
                    teamAxie.Add(AssetDatabase.LoadAssetAtPath<Character>(teamAxiePaths[i]));
                }
                else
                {
                    teamAxie[i] = AssetDatabase.LoadAssetAtPath<Character>(teamAxiePaths[i]);
                }
            }
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }

    [SerializeField]
    private void Start()
    {
        foreach (Character axie in teamAxie)
        {
            if (axie != null)
            {
                int index = teamAxie.IndexOf(axie);
                GameObject skeletonObject = new GameObject("SkeletonGraphic");
                SkeletonGraphic skeletonUI = skeletonObject.AddComponent<SkeletonGraphic>();
                skeletonUI.rectTransform.localScale = new Vector2(0.5f, 0.5f);
                Vector2 pos = skeletonUI.rectTransform.anchoredPosition;
                skeletonUI.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y - 30f);
                skeletonUI.skeletonDataAsset = axie.skeletonDataAsset;
                Instantiate(skeletonUI, buttonSlot[index].transform);
            }
        }
    }

    public void AddAxie(Character newAxie)
    {
        bool isDuplicate = false;
        foreach (Character axie in ownedAxie)
        {
            if (axie.isAxie == true && axie.axieId == newAxie.axieId)
            {
                isDuplicate = true;
            }
        }

        if (!isDuplicate)
        {
            bool isEmpty = true;
            for (int i = 0; i < ownedAxie.Count; i++)
            {
                if (ownedAxie[i] == null)
                {
                    ownedAxie[i] = newAxie;
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty)
            {
                ownedAxie.Add(newAxie);
            }
        }

        SaveData();
    }

    public void RemoveAxie(string id)
    {
        ownedAxie.RemoveAll(x => x.axieId == id);
    }

    //assume that the team in axie is full
    public void SwapAxiePos(int selectedPos, int replacePos)
    {
        Character temp = teamAxie[selectedPos];
        teamAxie[selectedPos] = teamAxie[replacePos];
        teamAxie[replacePos] = temp;
    }

    //assume that the team slot dont have any axie
    public void AddAxiePos(Character selectedAxie, int selectedPos)
    {
        teamAxie[selectedPos] = selectedAxie;
    }
}
