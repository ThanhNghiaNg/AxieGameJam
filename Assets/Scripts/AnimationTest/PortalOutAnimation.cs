using UnityEngine;

public class PortalOutAnimation : MonoBehaviour
{
    public static PortalOutAnimation Instance { get; private set; }

    public bool isTeleported { get; private set; }

    private void Awake()
    {
        isTeleported = true;
    }
}
