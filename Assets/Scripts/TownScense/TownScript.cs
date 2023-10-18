using UnityEngine;

public class TownScript : MonoBehaviour
{
    public string blockName;
    public int blockId;
    public Canvas lmaoCanvas;
    public Canvas whoAskCanvas;

    private void OnMouseDown()
    {
        switch (blockId)
        {
            case 0:
                {
                    Debug.Log("Who ask?");
                    lmaoCanvas.GetComponent<Canvas>().enabled = false;
                    whoAskCanvas.GetComponent<Canvas>().enabled = true;
                    break;
                }
            case 1:
                {
                    Debug.Log("Lmao");
                    whoAskCanvas.GetComponent<Canvas>().enabled = false;
                    lmaoCanvas.GetComponent<Canvas>().enabled = true;
                    break;
                }
        }
    }
}
