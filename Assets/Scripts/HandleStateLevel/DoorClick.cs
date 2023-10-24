using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class DoorClick : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    private bool isTouching = false;
    public float speed = 5.0f;
    public float duration = 1.0f;
    private void Awake()
    {
        skeletonAnimation = FindObjectOfType<SkeletonAnimation>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isTouching = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isTouching = false;
        }
    }
    private void OnMouseDown()
    {
        if (isTouching)
        {
            GameObject player = GameObject.FindWithTag("player");
            if (gameObject.transform.position.x - player.transform.position.x < 0)
            {
                player.transform.eulerAngles = Vector3.zero;
            }
            else
            {
                player.transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            StartCoroutine(ExitArea(player.transform));
        }
    }
    private IEnumerator ExitArea(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        GameManager.Instance.platformsParent.GetComponent<PlatformMovement>().enabled = false;
        GameObject[] backgroundObjects = GameObject.FindGameObjectsWithTag("Background");
        foreach (GameObject backgroundObject in backgroundObjects)
        {
            backgroundObject.GetComponent<LoopingBackground2D>().enabled = false;
        }

        skeletonAnimation.AnimationName = "action/run";
        yield return MoveTo(player, gameObject.transform.position);

        player.gameObject.SetActive(false);

        MoveToRoom(player);

        // GameManager.Instance.SetPlayerMovable(true);
        GameManager.Instance.SetStateRange(0, 1);
        GameManager.Instance.SetPlayerInRoom();
        GameManager.Instance.SetInitPositions(GameManager.Instance.endPosition, GameManager.Instance.endPosition);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.5f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }

    private void MoveToRoom(Transform player)
    {
        player.gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;

        GameObject spawnPos = GameObject.FindWithTag("spawn");
        Camera.main.transform.position = new Vector3(0, 0, -10);
        player.transform.position = spawnPos.transform.position;
        // StartCoroutine(ScalePortal());
    }
}
