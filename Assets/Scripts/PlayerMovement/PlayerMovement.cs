using Spine.Unity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 5f;
    public float animationTime = 3f;
    private SkeletonAnimation skeletonAnimation;
    private Vector2 position;
    private Vector2 startPosition;
    private float inputAxis;

    void Awake()
    {
        // skeletonAnimation = FindObjectOfType<SkeletonAnimation>();
        var player = transform.Find("player").transform;
        skeletonAnimation = player.GetComponent<SkeletonAnimation>();
        startPosition = transform.position;
        Debug.Log("skeletonAnimation");
        Debug.Log(skeletonAnimation);
    }

    // Update is called once per frame

    void Update()
    {
        inputAxis = Input.GetAxis("Horizontal");
        TurnAround();
        handleMovement();
        // Debug.Log($"SkeletonAnimation: {skeletonAnimation.AnimationName}");
        // position = transform.position;
    }
    void TurnAround()
    {
        float curDirection = inputAxis > 0f ? 1f : inputAxis < 0f ? -1 : 0;
        if (curDirection < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (curDirection > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void handleMovement()
    {
        if (GameManager.Instance.playerMovable)
        {
            if (transform.position.x < startPosition.x && !GameManager.Instance.isInRoom)
            {
                GameManager.Instance.SetBackgroundHallwayMovable(true);
                GameManager.Instance.SetPlayerMovable(false);
                transform.position = new Vector2(startPosition.x, transform.position.y);
                return;
            };

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

            transform.position = new Vector3(transform.position.x + inputAxis * moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        }
    }
}
