using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int playerStep { get; private set; }

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

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        playerStep = 0;
    }

    public void UpdateStep(float step)
    {
        // Debug.Log("step: " + step.ToString());
        playerStep = (int)step;
        // if (playerStep < 0f)
        // {
        //     playerStep = 0;
        // }
        // else if (playerStep > 9f)
        // {
        //     playerStep = 9;
        // }
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
