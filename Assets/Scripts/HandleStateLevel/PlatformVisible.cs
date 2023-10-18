using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVisible : MonoBehaviour
{
    void OnBecameVisible()
    {

        if (transform.localPosition.x / GameManager.Instance.spacing >= GameManager.Instance.stepRangeEnd + 1)
        {
            GameManager.Instance.SetBackgroundHallwayMovable(false);
            GameManager.Instance.SetPlayerMovable(true);
        }
    }
    void OnBecameInvisible()
    {
        if (transform.localPosition.x / GameManager.Instance.spacing >= GameManager.Instance.stepRangeEnd + 1)
        {
            GameManager.Instance.SetBackgroundHallwayMovable(true);
            GameManager.Instance.SetPlayerMovable(false);
        }
    }

}
