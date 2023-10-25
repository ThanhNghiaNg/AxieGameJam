using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    static public TeamManager Instance { get; private set; }

    public List<Character> ownedAxie;

    public List<Character> teamAxie;

    public Character Axie;

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

    public void AddAxie(string id)
    {
        bool isDuplicate = false;
        foreach (Character axie in ownedAxie)
        {
            if (axie.isAxie == true && axie.axieId == id)
            {
                isDuplicate = true;
            }
        }

        if (!isDuplicate)
        {
            foreach (Character Axie in AxieManager.Instance.characters)
            {
                if (Axie.isAxie && Axie.axieId == id)
                {
                    ownedAxie.Add(Axie);
                    break;
                }
            }

        }
    }

    public void RemoveAxie(string id)
    {
        ownedAxie.RemoveAll(x => x.axieId == id);
    }

    //assume that the team in axie is full
    public void MoveAxiePos(int selectedPos, int replacePos)
    {
        Character temp = teamAxie[selectedPos];
        teamAxie[selectedPos] = teamAxie[replacePos];
        teamAxie[replacePos] = temp;
    }

    //assume that the team slot dont have any axie
    public void AddAxiePos(Character selectedAxie, int selectedPos)
    {
        teamAxie[selectedPos] = selectedAxie;
    }
}
