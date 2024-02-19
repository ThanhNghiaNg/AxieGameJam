
using Spine.Unity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitManagement : MonoBehaviour
{
    public static RecruitManagement Instance { get; private set; }
    public List<Transform> recruitButton;
    public Transform container;
    private Transform AxieCard;
    private float startX = -250f;
    private float startY = 2f;

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
        AxieCard = container.Find("RecruitCard");
        AxieCard.gameObject.SetActive(false);
    }

    private bool IsExit(Character axie)
    {
        foreach(Character teamAxie in TeamManager.Instance.ownedAxie)
        {
            if(axie.axieId == teamAxie.axieId)
            {
                return true;
            }
        }
        return false;
    }

    private void Start()
    {
        foreach(Character axie in AxieManager.Instance.characters)
        {
            CreateCardButton(axie.skeletonDataAsset, axie, axie.axieName, axie.axieCost);
        }

        foreach (Character axie in AxieManager.Instance.characters)
        {
            if (IsExit(axie))
            {
                int index = AxieManager.Instance.characters.IndexOf(axie);
                recruitButton[index].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void AddAxie(Character newAxie)
    {
         TeamManager.Instance.AddAxie(newAxie);
    }

    public void UpdateRecruit()
    {
        foreach (Character axie in AxieManager.Instance.characters)
        {
            if (IsExit(axie))
            {
                int index = AxieManager.Instance.characters.IndexOf(axie);
                recruitButton[index].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void CreateCardButton(SkeletonDataAsset axieSprite, Character newAxie, string axieName, int price)
    {
        Transform axieCardTransform = Instantiate(AxieCard, container);
        RectTransform axieCardRectTransform = axieCardTransform.GetComponent<RectTransform>();
        axieCardRectTransform.anchoredPosition = new Vector2(startX, startY);
        startX += 180;

        axieCardTransform.Find("AxieName").GetComponent<TextMeshProUGUI>().SetText(axieName);
        axieCardTransform.Find("AxiePrice").GetComponent<TextMeshProUGUI>().SetText(price.ToString() + "$");
        axieCardTransform.Find("AxieSprite").GetComponent<Axie>().axie = newAxie;
        axieCardTransform.Find("AxieSprite").GetComponent<SkeletonGraphic>().skeletonDataAsset = axieSprite;
        axieCardTransform.Find("AxieSprite").GetComponent<SkeletonGraphic>().Initialize(true);
        axieCardTransform.gameObject.SetActive(true);
        recruitButton.Add(axieCardTransform);
    }
}
