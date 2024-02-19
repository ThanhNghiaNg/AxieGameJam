using UnityEngine;
using UnityEngine.UI;

public class TeamCircleUI : MonoBehaviour
{
    public Character Axie;
    public int position;
    public Button yourButton;
    public Canvas selectAxieCanvas;

    private void Awake()
    {
        selectAxieCanvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        TeamManager.Instance.currentSelectedSlot = position;
        selectAxieCanvas.gameObject.SetActive(true);
        selectAxieCanvas.gameObject.GetComponent<Canvas>().enabled = true;
    }
}
