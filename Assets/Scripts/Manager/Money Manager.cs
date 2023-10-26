using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int TotalMoney;
    public bool isEnough;

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

    public void AddMoney(int money)
    {
        TotalMoney += money;
    }

    public void MinusMoney(int money)
    {
        if(money <= TotalMoney)
        {
            isEnough = true;
            TotalMoney -= money;
        }
        else if (money > TotalMoney)
        {
            isEnough = false;
        }
    }
}
