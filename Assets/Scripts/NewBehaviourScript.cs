using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> platforms;
    public GameObject doorPlatform;
    public GameObject platform;
    public GameObject platformsParent;
    public GameObject currentPlatform;
    public GameObject prePlatform;

    public GameObject nextPlatform;

    float spacing = 5f;
    void Start()
    {
        GenPlatform(Random.Range(0,8));
        Load2();

    }

    void GenPlatform(int door)
    {
       for(int i = 0; i < 10; i++) {
            if(i == door || i == 9 ) 
            { 
                platforms.Add(doorPlatform);
                Debug.Log("I have no ideal?");
            }
            else platforms.Add(platform);
        }
    }
   public void LoadPlatform(int i, int _currentPlatform)
    {
       if(_currentPlatform == 0) {
            currentPlatform = Instantiate(platforms[_currentPlatform], platformsParent.transform.position, Quaternion.identity);
            nextPlatform = Instantiate(platforms[_currentPlatform+1], platformsParent.transform.position, Quaternion.identity);
        }
       else if ( i > 0)
       {
            prePlatform = currentPlatform;
            currentPlatform = nextPlatform;
            nextPlatform = Instantiate(platforms[_currentPlatform+1], platformsParent.transform.position, Quaternion.identity);
       }
       else
        {
            nextPlatform = currentPlatform;
            currentPlatform = prePlatform;
            prePlatform = Instantiate(platforms[_currentPlatform -1], platformsParent.transform.position, Quaternion.identity);
        }

        currentPlatform.transform.parent = platformsParent.transform;
        nextPlatform.transform.parent = platformsParent.transform;
        currentPlatform.transform.parent = platformsParent.transform;
    }
    public void Load2 ()
    {
        for (int i = 0; i < platforms.Count; i ++ )
        {
            var gameObj = Instantiate(platforms[i], platformsParent.transform.position + i * spacing * transform.right, Quaternion.identity);
            gameObj.transform.parent = platformsParent.transform;

        }
    }
   

    
}
