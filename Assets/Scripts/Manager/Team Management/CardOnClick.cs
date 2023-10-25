using UnityEngine;
using UnityEngine.UI;

public class CardOnClick : MonoBehaviour
{
    public new GameObject gameObject;
    private Character character;
    public Button yourButton;
    public Button[] buttonSlot;

    private void Awake()
    {
        character = gameObject.GetComponent<Axie>().axie;
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
                    //buttonSlot[0].GetComponent<Image>().sprite = ???
                    break;
                }
            case "1":
                {
                    break;
                }
            case "2":
                {
                    break;
                }
            case "3":
                {
                    break;
                }
        }
    }
}
