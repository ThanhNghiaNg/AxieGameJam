using UnityEngine;
using UnityEngine.UI;

public class TeamCircleUI : MonoBehaviour
{
    public Character Axie;
    public int position;

    public Button yourButton;

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        TeamManager.Instance.currentSelectedSlot = position;
    }
}
