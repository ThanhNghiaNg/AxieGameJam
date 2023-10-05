using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float padding = 2f;
    [SerializeField] float moveSpeed = 5f;
    SkeletonAnimation skeletonAnimation;
    public float animationTime = 3f;
    private new Vector2 position;
    private new Vector2 startPosition;
    private float direction = 1f;
    private float inputAxis;

    void Awake()
    {
        skeletonAnimation = FindObjectOfType<SkeletonAnimation>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        inputAxis = Input.GetAxis("Horizontal");
        TurnAround();
        handleMovement();
    }
    void TurnAround()
    {
        float curDirection = inputAxis > 0f ? 1f : inputAxis < 0f ? -1 : 0;
        if (curDirection > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (curDirection < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void handleMovement()
    {
        if (GameManager.Instance.playerMovable)
        {
            // if (transform.position.x >= startPosition.x + 1) {

            //     return;
            // };
            if (animationTime > 0)
            {
                animationTime -= 1f;
                return;
            }
            if (inputAxis == 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    skeletonAnimation.AnimationName = "attack/ranged/cast-tail";
                    skeletonAnimation.loop = false;
                    animationTime = 200f;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    skeletonAnimation.AnimationName = "attack/ranged/cast-multi";
                    skeletonAnimation.loop = false;
                    animationTime = 200f;
                }
                else
                {
                    skeletonAnimation.AnimationName = "action/idle/normal";
                    animationTime = 0f;
                    skeletonAnimation.loop = true;
                }
            }
            else
            {
                skeletonAnimation.AnimationName = inputAxis == 0 ? "action/idle/normal" : "action/run";
                animationTime = 0f;
                skeletonAnimation.loop = true;
            }


            transform.position = new Vector2(transform.position.x + inputAxis * moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}
