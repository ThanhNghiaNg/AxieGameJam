using Spine.Unity;
using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{
    private float inputAxis;
    public float moveSpeed = 8f;
    SkeletonAnimation skeletonAnimation;
    public float animationTime = 3f;

    void Awake()
    {
        skeletonAnimation = FindObjectOfType<SkeletonAnimation>();
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
/*        if (GameManager.Instance.playerMovable)
        {
            transform.position = new Vector2(transform.position.x + inputAxis * moveSpeed * Time.deltaTime, transform.position.y);
        }*/

        transform.position = new Vector2(transform.position.x + inputAxis * moveSpeed * Time.deltaTime, transform.position.y);
    }

    private void Update()
    {
        handleMovement();
        HorizontalMovement();
        TurnAround();
    }

    void TurnAround()
    {
        if (inputAxis < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (inputAxis > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void handleMovement()
    {
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
    }
}
