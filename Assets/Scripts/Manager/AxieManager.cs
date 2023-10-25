using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

public class AxieManager : MonoBehaviour
{
    public static AxieManager Instance { get; private set; }

    public List<Character> characters;

    public List<SkeletonDataAsset> skeletonDataAsset;

    private void Awake()
    {
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
}
