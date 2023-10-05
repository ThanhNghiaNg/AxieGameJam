using Spine.Unity;
using System.Collections;
using UnityEngine;

public class EventClick : MonoBehaviour
{
    public float speed = 5.0f;
    SkeletonAnimation skeletonAnimation;

    private void Awake()
    {
        skeletonAnimation = FindObjectOfType<SkeletonAnimation>();
    }
    private void OnMouseDown()
    {
        GameObject player = GameObject.FindWithTag("Player");
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

    private IEnumerator ExitArea(Transform player)
    {
        player.GetComponent<PlayerMovementSimple>().enabled = false;

        skeletonAnimation.AnimationName = "action/run";
        yield return MoveTo(player, gameObject.transform.position);

        player.gameObject.SetActive(false);

        MoveToRoom(player);

        GameManager.Instance.SetPlayerMovable(true);
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
        player.GetComponent<PlayerMovementSimple>().enabled = true;

        GameObject spawnPos = GameObject.FindWithTag("spawn");
        GameObject cameraPos = GameObject.FindWithTag("RoomPos");
        Camera.main.transform.position = cameraPos.transform.position;
        player.transform.position = spawnPos.transform.position;
    }
}
