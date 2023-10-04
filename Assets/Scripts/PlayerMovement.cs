using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float padding = 2f;
    [SerializeField] float moveSpeed = 5f;
    public float camWidth;

    public new Vector2 position;
    public float camHeight;
    public float direction = 1f;
    public float inputAxis;
    Camera cam;
    void Awake()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        inputAxis = Input.GetAxis("Horizontal");
        goNextRoom();
        TurnAround();
        handleMovement();
    }
    void TurnAround()
    {
        float curDirection = inputAxis > 0f ? 1f : inputAxis < 0f ? -1 : 0;
        if (curDirection * direction < 0f && curDirection * direction != 0)
        {
            transform.Rotate(0, 180, 0);
        }
        direction = curDirection;
    }
    void goNextRoom()
    {
        if (GameManager.Instance.playerStep == 9)
        {
            GameObject backgroundHallway = GameObject.FindWithTag("hallway");
            GameObject backgroundRoom = GameObject.FindWithTag("room");


            cam.transform.position = new Vector3(52.67f, cam.transform.position.y, cam.transform.position.z);
            transform.position = new Vector2(42.12f, transform.position.y);
            GameManager.Instance.UpdateStep(0f);
            GameManager.Instance.SetPlayerMovable(true);
            GameObject playerOnMiniMap = GameObject.FindWithTag("PlayerOnMiniMap");
            playerOnMiniMap.GetComponent<MovePlayerOnMap>().enabled = false;

            backgroundHallway.GetComponent<LoopingBackground2D>().enabled = false;
            // backgroundRoom.GetComponent<BoxCollider2D>().enabled = true;

            foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("skybox"))
            {
                fooObj.GetComponent<EdgeCollider2D>().enabled = true;
            }
        }
    }
    void handleMovement()
    {
        if (GameManager.Instance.playerMovable)
        {
            transform.position = new Vector2(transform.position.x + inputAxis * moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}
