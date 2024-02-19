using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Vector3 startPosition;
    void Awake()
    {

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            if (!GameManager.Instance.isInRoom)
            {
                GameManager.Instance.UpdateStep((int)(other.gameObject.transform.localPosition.x / GameManager.Instance.spacing));
            }
        }
    }
}
