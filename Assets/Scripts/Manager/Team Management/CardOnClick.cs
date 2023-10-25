using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class CardOnClick : MonoBehaviour
{
    public GameObject axieSprite;
    private Character character;
    public Button yourButton;
    public Button[] buttonSlot;
    private SkeletonGraphic skeletonGraphic;

    private void Awake()
    {
        character = axieSprite.GetComponent<Axie>().axie;
        skeletonGraphic = gameObject.AddComponent<SkeletonGraphic>();
        skeletonGraphic.skeletonDataAsset = character.axiePrefab.GetComponent<SkeletonAnimation>().skeletonDataAsset;
        skeletonGraphic.Initialize(true);
    }

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        string axieId = character.axieId;
        switch (axieId)
        {
            case "0":
                {
                    Instantiate(skeletonGraphic, buttonSlot[0].transform);
                    break;
                }
            case "1":
                {
                    Instantiate(skeletonGraphic, buttonSlot[1].transform);
                    break;
                }
            case "2":
                {
                    Instantiate(skeletonGraphic, buttonSlot[2].transform);
                    break;
                }
            case "3":
                {
                    Instantiate(skeletonGraphic, buttonSlot[3].transform);
                    break;
                }
        }
    }
}
