using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    [Range(-5f, 5f)]
    public float scrollSpeed = 2f;
    private float moveAmount;
    private float offset;
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        moveAmount = Input.GetAxis("Horizontal") * (Time.deltaTime * scrollSpeed) / 10f;
        offset = offset + moveAmount;
        material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
    }
}
