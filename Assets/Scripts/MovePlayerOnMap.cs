using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnMap : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float moveAmount;
    public float currentMove = 0;
    public int wholeCurrentMove;
    public float x;
    private float firstX;
    // GameManager gameManager;
    void Start()
    {
        firstX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float inputAxis = Input.GetAxis("Horizontal");
        moveAmount = inputAxis*moveSpeed;
        int direction = inputAxis > 0 ? 1 : -1;
        currentMove += moveAmount;
        wholeCurrentMove = (int)currentMove;
        x = wholeCurrentMove / 200;
        if ((GameManager.Instance.playerStep == 0 && inputAxis < 0) || (GameManager.Instance.playerStep == 9 && inputAxis > 0))
        {
            currentMove -= moveAmount;
        }
        else
        {
            transform.position = new Vector2(Mathf.Max(firstX, firstX + x), transform.position.y);
        }

        if (!(x < 0f || x > 9f))
        {
            GameManager.Instance.UpdateStep(x);
        }

    }
}
