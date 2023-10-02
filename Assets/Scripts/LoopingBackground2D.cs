using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground2D : MonoBehaviour
{
    [Range(-40f, 40f)]
    public float scrollSpeed = 20f;
    private float moveAmount;
    public float offset;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        moveAmount = Input.GetAxis("Horizontal") * (Time.deltaTime * scrollSpeed) / 10f;
        offset = offset + moveAmount; ;
        transform.position = new Vector3(-offset, transform.position.y);
        if (transform.position.x < -23.05f)
        {
            transform.position = startPosition;
            offset = 0f;
        }
       
    }
}
