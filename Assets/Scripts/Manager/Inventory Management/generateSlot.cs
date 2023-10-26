using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class generateSlot : MonoBehaviour
{
    private Transform container;
    private Transform ItemCard;
    private float startX = -270f;
    private float startY = 55f;

    private void Awake()
    {
        container = transform.Find("assetContainer");
        ItemCard = container.Find("ItemTemplate");
        ItemCard.gameObject.SetActive(false);
    }

    private void Start()
    {
        int index = 0;
        foreach (Item item in InventoryManager.Instance.Inventory)
        {
            if (item == null)
            {
                CreateCardButton(null, null, index);
            }
            else
            {
                int itemIndex = InventoryManager.Instance.Inventory.IndexOf(item);
                CreateCardButton(InventoryManager.Instance.Inventory[itemIndex].sprite, InventoryManager.Instance.Inventory[itemIndex].quatity, index);
            }
            index++;
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
            axieCardTransform.Find("quitity").GetComponent<TextMeshProUGUI>().SetText(quatity.ToString());
        }
        else
        {
            axieCardTransform.Find("Image").GetComponent<Image>().sprite = null;
            axieCardTransform.Find("Image").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            axieCardTransform.Find("quatity").GetComponent<TextMeshProUGUI>().SetText("");
        }
        axieCardTransform.gameObject.SetActive(true);
    }
}
