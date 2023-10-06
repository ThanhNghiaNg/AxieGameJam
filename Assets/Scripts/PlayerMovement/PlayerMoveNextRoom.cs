using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveNextRoom : MonoBehaviour
{
    Camera cam;
    public float camWidth;
    public float camHeight;
    void Start()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        goNextRoom();
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

            foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("skybox"))
            {
                fooObj.GetComponent<EdgeCollider2D>().enabled = true;
            }
        }
    }
}
