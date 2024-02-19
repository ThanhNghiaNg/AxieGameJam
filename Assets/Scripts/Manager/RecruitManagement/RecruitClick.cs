using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitClick : MonoBehaviour
{
    public Button yourButton;
    private Character axie;
    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void HideItem()
    {
        Tooltip.HideTooltip_Static();
    }

    private void TaskOnClick()
    {
        axie = yourButton.transform.Find("AxieSprite").GetComponent<Axie>().axie;
        MoneyManager.Instance.MinusMoney(axie.axieCost);
        if (MoneyManager.Instance.isEnough)
        {
            RecruitManagement.Instance.AddAxie(axie);
            RecruitManagement.Instance.UpdateRecruit();
        } else
        {
            System.Func<string> getTooltipTextFunc = () =>
            {
                return "<color=#FF0000>Not enough money</color>";
            };
            Tooltip.ShowTooltip_Static(getTooltipTextFunc);
            Invoke("HideItem", 1.5f);
        }
    }

}
