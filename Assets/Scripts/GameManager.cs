using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int playerStep { get; private set; }
    public int stepRangeStart { get; private set; }
    public int stepRangeEnd { get; private set; }
    public bool playerMovable { get; private set; }
    public bool backgroundHallwayMovable { get; private set; }

    private void Awake()
    {
        stepRangeStart = 0;
        stepRangeEnd = 9;
        playerMovable = false;
        backgroundHallwayMovable = true;
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
    public void SetBackgroundHallwayMovable(bool movable)
    {
        backgroundHallwayMovable = movable;
    }

    public void UpdateStep(float step)
    {
        playerStep = (int)step;
        if (playerStep == stepRangeEnd)
        {
            playerMovable = true;
            SetBackgroundHallwayMovable(false);
        }
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
