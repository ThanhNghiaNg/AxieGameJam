using UnityEngine;
using UnityEngine.UI;

public class mainCanvasScript : MonoBehaviour
{
    public Button yourButton;
    public Canvas mainCanvas;

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        mainCanvas.GetComponent<Canvas>().enabled = false;
    }
}
