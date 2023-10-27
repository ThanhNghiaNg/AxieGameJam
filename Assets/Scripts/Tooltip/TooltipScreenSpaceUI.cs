using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private System.Func<string> getTooltipTextFunc;

    private void Awake()
    {
        Instance = this;

        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();

        HideToolTip();
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();
        Vector2 paddingSize = new Vector2(8, 8);

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void Update()
    {
        SetText(getTooltipTextFunc());
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            //tooltip left screen or right side
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            //tooltip left screen or right side
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;

    }

    private void ShowToolTip(string tooltipText)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    private void ShowTooltip(System.Func<string> getTooltipTextFunc)
    {
        this.getTooltipTextFunc = getTooltipTextFunc;
        gameObject.SetActive(true);
        SetText(getTooltipTextFunc());
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    public static void HideTooltip_Static()
    {
        Instance.HideToolTip();
    }

    public static void ShowTooltip_Static(string tooltipText)
    {
        Instance.ShowToolTip(tooltipText);
    }

    public static void ShowTooltip_Static(System.Func<string> getTooltipTextFunc)
    {
        Instance.ShowTooltip(getTooltipTextFunc);
    }
}
