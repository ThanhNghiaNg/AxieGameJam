using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground2D : MonoBehaviour
{
    [Range(-20f, 20f)]
    public float scrollSpeed = 8f;
    private float moveAmount;
    private float offset;
    private Sprite sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }


    void LateUpdate()
    {
        Movement();
    }

    void Movement()
    {
        moveAmount = Input.GetAxis("Horizontal") * (Time.deltaTime * scrollSpeed) / 10f;
        offset = offset + moveAmount;
        // sprite.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        transform.position = new Vector3(-offset, transform.position.y);
    }
}
