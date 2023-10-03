using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground2D : MonoBehaviour
{
    [Range(-40f, 40f)]
    public float scrollSpeed = 20f;
    private float moveAmount;
    public float height;
    public float width;
    public float offset;
    private Vector3 startPosition;
    Camera cam;

    void Start()
    {
        startPosition = transform.position;
        cam = Camera.main;
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        float inputAxis = Input.GetAxis("Horizontal");
        moveAmount = inputAxis * (Time.deltaTime * scrollSpeed) / 10f;
        offset = offset + moveAmount;

        Vector3 nextPosition = new Vector3(-offset, transform.position.y);
        if ((GameManager.Instance.playerStep == 0 && inputAxis < 0) || (GameManager.Instance.playerStep == 9 && inputAxis > 0))
        {
            offset = offset - moveAmount;
            return;
        }
        else
        {
            transform.position = nextPosition;
        }

        transform.position = nextPosition;

        if (transform.position.x < -21.8f || transform.position.x > 21.8f)
        {
            transform.position = startPosition;
            offset = 0f;
        }

    }
}
