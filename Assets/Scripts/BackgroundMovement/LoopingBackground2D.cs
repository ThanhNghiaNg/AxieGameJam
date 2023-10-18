using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground2D : MonoBehaviour
{
    // [Range(-40f, 40f)]
    public float scrollSpeed = 20f;
    public float loopPointX = 56.96f;
    public bool isAbleToLoop = true;
    private float moveAmount = 0f;
    private float height = 0f;
    private float width = 0f;
    private float offset = 0f;
    private float totalMove = 0f;
    private Vector3 startPosition;
    Camera cam;

    void Awake()
    {
        startPosition = transform.position;
    }

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (GameManager.Instance.backgroundHallwayMovable == false) return;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        float inputAxis = Input.GetAxis("Horizontal");

        moveAmount = inputAxis * (Time.deltaTime * scrollSpeed) / 10f;
        offset = offset + moveAmount;

        Vector3 nextPosition = new Vector3(-offset, transform.position.y, transform.position.z);
        if ((GameManager.Instance.playerStep == 0 && inputAxis < 0 && startPosition.x < transform.position.x) || (GameManager.Instance.playerStep == 9 && inputAxis > 0))
        {
            totalMove -= moveAmount;
            offset = offset - moveAmount;
            return;
        }
        else
        {
            totalMove += moveAmount;
            transform.position = nextPosition;
        }

        transform.position = nextPosition;

        if (Mathf.Abs(transform.position.x) >= loopPointX && isAbleToLoop)
        {
            transform.position = startPosition;
            offset = 0f;
        }

    }
}
