using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVisible : MonoBehaviour
{
    void OnBecameVisible()
    {

        if (transform.localPosition.x / GameManager.Instance.spacing >= GameManager.Instance.stepRangeEnd + 1)
        {
            Debug.Log("localPosition: " + transform.localPosition.ToString());
            GameManager.Instance.SetBackgroundHallwayMovable(false);
            GameManager.Instance.SetPlayerMovable(true);
            Debug.Log("Appear: " + GameManager.Instance.backgroundHallwayMovable.ToString());
        }
    }
    void OnBecameInvisible()
    {
        if (transform.localPosition.x / GameManager.Instance.spacing >= GameManager.Instance.stepRangeEnd + 1)
        {
            Debug.Log("localPosition: " + transform.localPosition.ToString());
            Debug.Log("Disappear");
            GameManager.Instance.SetBackgroundHallwayMovable(true);
            GameManager.Instance.SetPlayerMovable(false);
        }
    }

}
