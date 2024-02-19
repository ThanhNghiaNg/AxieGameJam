using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerActionInBackground : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        skeletonAnimation = player.transform.Find("player").transform.GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputAxis = Input.GetAxis("Horizontal");
        skeletonAnimation.AnimationName = inputAxis == 0 ? "action/idle/normal" : "action/run";
    }
}
