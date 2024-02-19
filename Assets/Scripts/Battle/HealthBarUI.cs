using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text healthBarText;
    
    public void DisplayHealth(int healthAmount)
    {
        healthBar.value = healthAmount;
    }
}
