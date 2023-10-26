using Spine.Unity;
using TMPro;
using UnityEngine;

public class SelectAxie : MonoBehaviour
{
    private Transform container;
    private Transform AxieCard;
    private float startX = -250f;
    private float startY = 2f;

    private void Awake()
    {
        container = transform.Find("selectAxieContainer");
        AxieCard = container.Find("AxieCard");
        AxieCard.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateCardButton(AxieManager.Instance.skeletonDataAsset[0], AxieManager.Instance.characters[0], "Aqua");
        CreateCardButton(AxieManager.Instance.skeletonDataAsset[1], AxieManager.Instance.characters[1], "Bqua");
        CreateCardButton(AxieManager.Instance.skeletonDataAsset[2], AxieManager.Instance.characters[2], "Kqua");
        CreateCardButton(AxieManager.Instance.skeletonDataAsset[3], AxieManager.Instance.characters[3], "Hqua");
    }

    private void CreateCardButton(SkeletonDataAsset axieSprite, Character newAxie, string axieName)
    {
        Transform axieCardTransform = Instantiate(AxieCard, container);
        RectTransform axieCardRectTransform = axieCardTransform.GetComponent<RectTransform>();
        axieCardRectTransform.anchoredPosition = new Vector2(startX, startY);
        startX += 180;

        axieCardTransform.Find("AxieName").GetComponent<TextMeshProUGUI>().SetText(axieName);
        axieCardTransform.Find("AxieSprite").GetComponent<Axie>().axie = newAxie;
        axieCardTransform.Find("AxieSprite").GetComponent<SkeletonGraphic>().skeletonDataAsset = axieSprite;
        axieCardTransform.Find("AxieSprite").GetComponent<SkeletonGraphic>().Initialize(true);
        axieCardTransform.gameObject.SetActive(true);
    }
}
