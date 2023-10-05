using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class LoopingBackground2D : MonoBehaviour
{
    [Range(-40f, 40f)]
    public float scrollSpeed = 20f;
    private float moveAmount;
    public float height;
    public float width;
    public float offset;
    public float totalMove = 0;
    private Vector3 startPosition;
    private SkeletonAnimation skeletonAnimation;
    Camera cam;

    void Start()
    {
        startPosition = transform.position;
        cam = Camera.main;
        skeletonAnimation = FindObjectOfType<SkeletonAnimation>();
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
        skeletonAnimation.AnimationName = inputAxis == 0 ? "action/idle/normal" : "action/run";

        moveAmount = inputAxis * (Time.deltaTime * scrollSpeed) / 10f;
        offset = offset + moveAmount;

        Vector3 nextPosition = new Vector3(-offset, transform.position.y);
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

        if (Mathf.Abs(transform.position.x) >= 26.25f)
        {
            transform.position = startPosition;
            offset = 0f;
        }

    }
}
