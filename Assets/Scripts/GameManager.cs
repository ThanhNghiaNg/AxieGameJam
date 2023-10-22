using JetBrains.Annotations;
using System.Collections;
using Newtonsoft.Json;
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

    #region Handle player movement state
    public int[] currentPosition;
    public int[] startPosition;
    public int[] endPosition;

    #endregion

    #region Generate Platform State
    public List<GameObject> platforms;
    public GameObject doorPlatform;
    public GameObject platform;
    public GameObject platformsParent;
    public GameObject currentPlatform;
    public GameObject prePlatform;
    public bool isInRoom = false;
    public GameObject nextPlatform;

    public float spacing = 5f;
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
        // Generate();
    }

    public void clearAllPlatforms()
    {
        platforms.Clear();
        foreach (Transform platform in platformsParent.transform)
        {
            Destroy(platform.gameObject);
        }
    }
    public void PlatformGenerate()
    {
        clearAllPlatforms();
        NewGame();
        GenPlatform(Random.Range(stepRangeStart + 1, stepRangeEnd - 1));
        Load2();
        platformsParent.transform.position = new Vector2(-6.4f, -2.05f);
    }

    public void SetStateRange(int start, int end)
    {
        stepRangeStart = start;
        stepRangeEnd = end;
    }
    public void SetInitPositions(int[] start, int[] end)
    {
        startPosition = start;
        endPosition = end;
        Debug.Log($"startPosition: {JsonConvert.SerializeObject(startPosition)}, endPosition : {JsonConvert.SerializeObject(endPosition)}");        
        int distance = Mathf.Abs(startPosition[0] - endPosition[0]) + Mathf.Abs(startPosition[1] - endPosition[1]);
        SetStateRange(0, distance);
        PlatformGenerate();
    }

    public void UpdatePosition(int step)
    {

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
        
        if (LoadTilemapFromJson.Instance.segments != null)
        {
            currentPosition = LoadTilemapFromJson.Instance.segments[playerStep];
        }

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
        for (int i = stepRangeStart; i <= (isInRoom == true ? 3 : stepRangeEnd+1); i++)
        {
            // if (i == door || i == stepRangeEnd)
            Debug.Log($"i: {i.ToString()}, stepRangeStart: {stepRangeStart}, stepRangeEnd: {stepRangeEnd}");
            if (i == stepRangeEnd && isInRoom == false)
            {
                platforms.Add(doorPlatform);
            }
            else platforms.Add(platform);
        }
    }

    public void Load2()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            var gameObj = Instantiate(platforms[i], platformsParent.transform.position + i * spacing * transform.right, Quaternion.identity);
            gameObj.tag = "platform";
            gameObj.AddComponent<PlatformVisible>();
            gameObj.transform.parent = platformsParent.transform;
        }
    }

    public void SetPlayerInRoom(){
        isInRoom = true;
    }
    public void SetPlayerInHallway(){
        isInRoom = false;
    }
}
