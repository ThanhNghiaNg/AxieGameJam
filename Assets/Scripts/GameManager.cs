using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int playerStep { get; private set; }

    public bool playerMovable { get; private set; }
    private void Awake()
    {
        playerMovable = false;
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

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        playerStep = 0;
    }

    public void SetPlayerMovable(bool movable)
    {
        playerMovable = movable;
    }

    public void UpdateStep(float step)
    {
        playerStep = (int)step;
    }
    public void increaseStep()
    {
        playerStep++;
    }

    public void decreaseStep()
    {
        playerStep--;
    }
}
