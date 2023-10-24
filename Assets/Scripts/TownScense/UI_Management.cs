using UnityEngine;

public class UI_Management : MonoBehaviour
{
    public static UI_Management Instance { get; private set; }
    public bool isClicked = false;
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}
