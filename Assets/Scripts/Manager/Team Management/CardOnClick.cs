using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CardOnClick : MonoBehaviour
{
    public GameObject axieSprite;
    [SerializeField]
    private Character character;
    public Button yourButton;
    public Button[] buttonSlot;
    [SerializeField]
    private SkeletonGraphic skeletonGraphic;
    public Canvas selectAxieCanvas;

    [SerializeField]
    private void Awake()
    {
        character = axieSprite.GetComponent<Axie>().axie;
        if (skeletonGraphic == null)
        {
            GameObject skeletonObject = new GameObject("SkeletonGraphic");
            skeletonGraphic = skeletonObject.AddComponent<SkeletonGraphic>();
            skeletonGraphic.rectTransform.localScale = new Vector2(0.5f, 0.5f);
            Vector2 pos = skeletonGraphic.rectTransform.anchoredPosition;
            skeletonGraphic.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y - 30f);
            skeletonGraphic.skeletonDataAsset = character.skeletonDataAsset;
        }
    }

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    [SerializeField]
    private bool isExit(Character axie)
    {
        foreach (Character teamAxie in TeamManager.Instance.teamAxie)
        {
            if (teamAxie != null)
            {
                if (axie.axieId == teamAxie.axieId)
                {
                    return true;
                }
            }
        }
        return false;
    }

    [SerializeField]
    private void updateTeamPos()
    {
        foreach (Character teamAxie in TeamManager.Instance.teamAxie)
        {
            if (teamAxie != null)
            {
                int index = TeamManager.Instance.teamAxie.IndexOf(teamAxie);
                GameObject skeletonObject = new GameObject("SkeletonGraphic");
                SkeletonGraphic skeletonUI = skeletonObject.AddComponent<SkeletonGraphic>();
                skeletonUI.rectTransform.localScale = new Vector2(0.5f, 0.5f);
                Vector2 pos = skeletonUI.rectTransform.anchoredPosition;
                skeletonUI.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y - 30f);
                skeletonUI.skeletonDataAsset = teamAxie.skeletonDataAsset;
                Instantiate(skeletonUI, buttonSlot[index].transform);
            }
            selectAxieCanvas.gameObject.SetActive(false);
        }
    }

    [SerializeField]
    private void TaskOnClick()
    {
        TeamManager.Instance.currentSelectedAxie = character;
        switch (TeamManager.Instance.currentSelectedSlot)
        {
            case 0:
                {
                    if (TeamManager.Instance.teamAxie[0] == null && isExit(character) == false)
                    {
                        Instantiate(skeletonGraphic, buttonSlot[0].transform);
                        TeamManager.Instance.teamAxie[0] = character;
                        selectAxieCanvas.gameObject.SetActive(false);
                    }
                    else if (isExit(character) && TeamManager.Instance.teamAxie[0] == null)
                    {
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[index].transform.DetachChildren();
                        TeamManager.Instance.teamAxie[0] = character;
                        TeamManager.Instance.teamAxie[index] = null;
                        updateTeamPos();
                    }
                    else if (TeamManager.Instance.teamAxie[0].axieId == character.axieId)
                    {
                        selectAxieCanvas.gameObject.SetActive(false);
                    }
                    else if (isExit(character) && TeamManager.Instance.teamAxie[0] != null)
                    {
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[0].transform.DetachChildren();
                        buttonSlot[index].transform.DetachChildren();
                        TeamManager.Instance.SwapAxiePos(index, TeamManager.Instance.currentSelectedSlot);
                        selectAxieCanvas.gameObject.SetActive(false);
                        updateTeamPos();
                    }

                    break;
                }
            case 1:
                {
                    if (TeamManager.Instance.teamAxie[1] == null && isExit(character) == false)
                    {
                        Instantiate(skeletonGraphic, buttonSlot[1].transform);
                        TeamManager.Instance.teamAxie[1] = character;
                        selectAxieCanvas.gameObject.SetActive(false);
                    }
                    else if (isExit(character) && TeamManager.Instance.teamAxie[1] == null)
                    {
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[index].transform.DetachChildren();
                        TeamManager.Instance.teamAxie[1] = character;
                        TeamManager.Instance.teamAxie[index] = null;
                        updateTeamPos();
                    }
                    else if (TeamManager.Instance.teamAxie[1].axieId == character.axieId)
                    {
                        selectAxieCanvas.gameObject.SetActive(false);
                    }

                    else if (isExit(character) && TeamManager.Instance.teamAxie[1] != null)
                    {
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[1].transform.DetachChildren();
                        buttonSlot[index].transform.DetachChildren();


                        TeamManager.Instance.SwapAxiePos(index, TeamManager.Instance.currentSelectedSlot);
                        selectAxieCanvas.gameObject.SetActive(false);
                        updateTeamPos();
                    }

                    break;
                }
            case 2:
                {
                    if (TeamManager.Instance.teamAxie[2] == null && isExit(character) == false)
                    {
                        buttonSlot[2].transform.DetachChildren();
                        Instantiate(skeletonGraphic, buttonSlot[2].transform);
                        TeamManager.Instance.teamAxie[2] = character;
                        selectAxieCanvas.gameObject.SetActive(false);
                    }
                    else if (TeamManager.Instance.teamAxie[2] == null && isExit(character))
                    {
                        buttonSlot[2].transform.DetachChildren();
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[index].transform.DetachChildren();
                        TeamManager.Instance.teamAxie[2] = character;
                        TeamManager.Instance.teamAxie[index] = null;
                        updateTeamPos();
                    }
                    else if (TeamManager.Instance.teamAxie[2].axieId == character.axieId)
                    {
                        selectAxieCanvas.gameObject.SetActive(false);
                    }
                    else if (isExit(character) && TeamManager.Instance.teamAxie[2] != null)
                    {
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[2].transform.DetachChildren();
                        buttonSlot[index].transform.DetachChildren();
                        TeamManager.Instance.SwapAxiePos(index, TeamManager.Instance.currentSelectedSlot);
                        selectAxieCanvas.gameObject.SetActive(false);
                        updateTeamPos();
                    }
                    break;
                }
            case 3:
                {
                    if (TeamManager.Instance.teamAxie[3] == null && isExit(character) == false)
                    {
                        Instantiate(skeletonGraphic, buttonSlot[3].transform);
                        TeamManager.Instance.teamAxie[3] = character;
                        selectAxieCanvas.gameObject.SetActive(false);
                    }
                    else if (isExit(character) && TeamManager.Instance.teamAxie[3] == null)
                    {
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[index].transform.DetachChildren();
                        TeamManager.Instance.teamAxie[3] = character;
                        TeamManager.Instance.teamAxie[index] = null;
                        updateTeamPos();
                    }
                    else if (TeamManager.Instance.teamAxie[3].axieId == character.axieId)
                    {
                        selectAxieCanvas.gameObject.SetActive(false);
                    }
                    else if (isExit(character) && TeamManager.Instance.teamAxie[3] != null)
                    {
                        int index = TeamManager.Instance.teamAxie.IndexOf(character);
                        buttonSlot[3].transform.DetachChildren();
                        buttonSlot[index].transform.DetachChildren();
                        TeamManager.Instance.SwapAxiePos(index, TeamManager.Instance.currentSelectedSlot);
                        selectAxieCanvas.gameObject.SetActive(false);
                        updateTeamPos();
                    }

                    break;
                }
        }
    }
}
