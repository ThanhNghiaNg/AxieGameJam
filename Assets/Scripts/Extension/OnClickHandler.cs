using UnityEngine;

public class OnClick : MonoBehaviour
{
    public static OnClick Instance { get; private set; }

    public GameObject TargetObject { get; private set; }

    private void Awake()
    {
        TargetObject = null;
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        ClickHandler();
    }

    public void SetTargetObject(GameObject other)
    {
        TargetObject = other;
    }

    public void ClickHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                SetTargetObject(hit.collider.gameObject);
                return;
            }
            TargetObject = null;
        }
    }
}
