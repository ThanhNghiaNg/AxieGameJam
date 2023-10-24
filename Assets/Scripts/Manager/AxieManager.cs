using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxieManager : MonoBehaviour
{
    public static AxieManager Instance { get; private set; }

    public List<Character> characters;

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
