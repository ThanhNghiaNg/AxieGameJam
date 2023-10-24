using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlayer : MonoBehaviour
{
    BattleManager battleManager;
    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }
    private void OnMouseDown()
    {
        Debug.Log("Done");
        if (battleManager.currentSkill != null)
        {
            battleManager.selectedCharacter = GetComponentInParent<CharacterUI>();
            battleManager.UsingSkill();
            battleManager.ChangeTurn();
        }
    }
}
