using Spine.Unity;
using System.Collections;
using UnityEngine;

public class EventClickMoveNextRoom : MonoBehaviour
{
    public float speed = 5.0f;
    SkeletonAnimation skeletonAnimation;
    private GameObject portal;
    private bool isTouching = false;

    public float duration = 1.0f;
    private void Awake()
    {
        // portal = GameObject.FindWithTag("portal");
        // portal.transform.localScale = new Vector3(1.8f, 0.8f, 0f);
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
        Debug.Log("?");
        Debug.Log(player.ToString());
        player.GetComponent<PlayerMovement>().enabled = false;
        Debug.Log("???");
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

        // StartCoroutine(ScalePortal());
    }

    private IEnumerator ScalePortal()
    {
        float elapsed = 0f;

        Vector3 startScale = portal.transform.localScale;
        Vector3 endScale = new Vector3(2.2f, 1f, 0f);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            portal.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        portal.transform.localScale = endScale;
    }
}
