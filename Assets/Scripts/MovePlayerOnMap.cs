using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnMap : MonoBehaviour
{
    [SerializeField] int framePerStep = 500;
    [SerializeField] float moveSpeed = 1f;
    private float moveAmount;
    private float currentMove = 0;
    private int wholeCurrentMove;

    private float x;
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
        moveAmount = inputAxis * moveSpeed;
        int direction = inputAxis > 0 ? 1 : -1;
        currentMove += moveAmount;
        if (currentMove > framePerStep * (GameManager.Instance.stepRangeEnd + 2) - 1) currentMove = framePerStep * (GameManager.Instance.stepRangeEnd + 2) - 1;
        wholeCurrentMove = (int)currentMove;
        x = wholeCurrentMove / framePerStep;
        if ((GameManager.Instance.playerStep == 0 && inputAxis < 0) || (GameManager.Instance.playerStep == 9 && inputAxis > 0))
        {
            if (transform.position.x <= firstX)
            {
                currentMove -= moveAmount;
            }
        }
        else
        {
            transform.position = new Vector2(Mathf.Max(firstX, firstX + GameManager.Instance.playerStep), transform.position.y);
        }
        
        if (x >= GameManager.Instance.stepRangeStart && x <= GameManager.Instance.stepRangeEnd)
        {
            GameManager.Instance.UpdateStep(x);
        }
    }
}
