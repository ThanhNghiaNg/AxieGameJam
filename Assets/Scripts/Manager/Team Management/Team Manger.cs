using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    static public TeamManager Instance { get; private set; }

    public List<Character> ownedAxie;

    [SerializeField]
    public List<Character> teamAxie;
    public Button[] buttonSlot;
    public Character currentSelectedAxie;
    public int currentSelectedSlot;

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

    [SerializeField]
    private void Start()
    {
        for (int i = 0; i < teamAxie.Count; i++)
        {
            if (teamAxie[i] != null)
            {
                int index = teamAxie.IndexOf(teamAxie[i]);
                GameObject skeletonObject = new GameObject("SkeletonGraphic");
                SkeletonGraphic skeletonUI = skeletonObject.AddComponent<SkeletonGraphic>();
                skeletonUI.rectTransform.localScale = new Vector2(0.5f, 0.5f);
                Vector2 pos = skeletonUI.rectTransform.anchoredPosition;
                skeletonUI.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y - 30f);
                skeletonUI.skeletonDataAsset = teamAxie[i].skeletonDataAsset;
                Instantiate(skeletonUI, buttonSlot[index].transform);
            }
        }
    }

    public void AddAxie(string id)
    {
        bool isDuplicate = false;
        foreach (Character axie in ownedAxie)
        {
            if (axie.isAxie == true && axie.axieId == id)
            {
                isDuplicate = true;
            }
        }

        if (!isDuplicate)
        {
            foreach (Character Axie in AxieManager.Instance.characters)
            {
                if (Axie.isAxie && Axie.axieId == id)
                {
                    ownedAxie.Add(Axie);
                    break;
                }
            }

        }
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
