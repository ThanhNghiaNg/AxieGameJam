using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Handle background movement state
    public static GameManager Instance { get; private set; }

    public bool _playerMovable;
    public int playerStep { get; private set; }
    public int stepRangeStart { get; private set; }
    public int stepRangeEnd { get; private set; }
    public bool playerMovable { get; private set; }
    public bool backgroundHallwayMovable { get; private set; }
    #endregion

    #region Generate Platform State
    public List<GameObject> platforms;
    public GameObject doorPlatform;
    public GameObject platform;
    public GameObject platformsParent;
    public GameObject currentPlatform;
    public GameObject prePlatform;

    public GameObject nextPlatform;

    float spacing = 5f;
    #endregion
    private void Awake()
    {
        stepRangeStart = 0;
        stepRangeEnd = 9;
        if (_playerMovable != null)
        {
            playerMovable = _playerMovable;
        }
        else
        {
            playerMovable = false;
        }

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
        GenPlatform(Random.Range(stepRangeStart + 1, stepRangeEnd - 1));
        Load2();
    }

    public void SetStateRange(int start, int end)
    {
        stepRangeStart = start;
        stepRangeEnd = end;
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
        if (playerStep < stepRangeStart) playerStep = stepRangeStart;
        if (playerStep > stepRangeEnd) playerStep = stepRangeEnd;
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


    void GenPlatform(int door)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i == door || i == 9)
            {
                platforms.Add(doorPlatform);
                Debug.Log("I have no ideal?");
            }
            else platforms.Add(platform);
        }
    }

    public void Load2()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            // Debug.Log("platform: " + (platformsParent.transform.position + i * spacing * transform.right).ToString());
            // var gameObj = Instantiate(platforms[i], platformsParent.transform.position + i * spacing * transform.right, Quaternion.identity);
            var gameObj = Instantiate(platforms[i], platformsParent.transform.position + i * spacing * transform.right, Quaternion.identity, platformsParent.transform);

            // gameObj.transform.parent = platformsParent.transform;

        }
        // platformsParent.GetComponent<PlatformMovement>().enabled = true;
        // platformsParent.transform.anchoredPosition = new Vector2(-7.15f, -3.8f);
    }


}
