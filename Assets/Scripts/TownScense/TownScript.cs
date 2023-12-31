using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TownScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string blockName;
    public int blockId;
    public Canvas shopCanvas;
    public Canvas recruitCanvas;
    public Canvas assetCanvas;
    public Canvas teamCanvas;
    public Button buttonUI;

    public GameObject[] Title;

    /*    private void OnMouseExit()
        {
            Title[1].SetActive(false);
            Title[2].SetActive(false);
            Title[3].SetActive(false);
            Title[0].SetActive(false);
        }*/

    public void OnPointerExit(PointerEventData eventData)
    {
        Title[1].SetActive(false);
        Title[2].SetActive(false);
        Title[3].SetActive(false);
        Title[0].SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (GameObject item in Title)
        {
            item.SetActive(false);
        }
        buttonUI.gameObject.SetActive(false);
        if (UI_Management.Instance.isClicked == false)
        {
            switch (blockId)
            {
                case 0:
                    {
                        shopCanvas.GetComponent<Canvas>().enabled = false;
                        assetCanvas.GetComponent<Canvas>().enabled = false;
                        teamCanvas.GetComponent<Canvas>().enabled = false;
                        recruitCanvas.GetComponent<Canvas>().enabled = true;
                        break;
                    }
                case 1:
                    {
                        recruitCanvas.GetComponent<Canvas>().enabled = false;
                        assetCanvas.GetComponent<Canvas>().enabled = false;
                        teamCanvas.GetComponent<Canvas>().enabled = false;
                        shopCanvas.GetComponent<Canvas>().enabled = true;
                        LoadShopItems.Instance.DisplayShopItems(shopCanvas);
                        break;
                    }
                case 2:
                    {
                        recruitCanvas.GetComponent<Canvas>().enabled = false;
                        shopCanvas.GetComponent<Canvas>().enabled = false;
                        teamCanvas.GetComponent<Canvas>().enabled = false;
                        assetCanvas.GetComponent<Canvas>().enabled = true;
                        break;
                    }
                case 3:
                    {
                        recruitCanvas.GetComponent<Canvas>().enabled = false;
                        shopCanvas.GetComponent<Canvas>().enabled = false;
                        assetCanvas.GetComponent<Canvas>().enabled = false;
                        teamCanvas.GetComponent<Canvas>().enabled = true;
                        break;
                    }
            }
        }

        UI_Management.Instance.isClicked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UI_Management.Instance.isClicked == false)
        {
            switch (blockId)
            {
                //recruit
                case 0:
                    {
                        Title[1].SetActive(false);
                        Title[2].SetActive(false);
                        Title[3].SetActive(false);
                        Title[0].SetActive(true);
                        break;
                    }
                //shop
                case 1:
                    {
                        Title[2].SetActive(false);
                        Title[0].SetActive(false);
                        Title[3].SetActive(false);
                        Title[1].SetActive(true);
                        break;
                    }
                //asset
                case 2:
                    {
                        Title[3].SetActive(false);
                        Title[0].SetActive(false);
                        Title[1].SetActive(false);
                        Title[2].SetActive(true);
                        break;
                    }
                //team
                case 3:
                    {
                        Title[2].SetActive(false);
                        Title[0].SetActive(false);
                        Title[1].SetActive(false);
                        Title[3].SetActive(true);
                        break;
                    }
            }
        }

    }
}
