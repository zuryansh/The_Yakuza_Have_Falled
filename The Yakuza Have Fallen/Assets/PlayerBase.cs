using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBase : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthSlider;
    public TextMeshProUGUI healthText;
    public int speedPickupNum;
    public int maxSpeedNum;
    public int coinNum;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }



    public void UpdateUi()
    {
        healthSlider.SetHealth(currentHealth);
        healthText.text = $"{currentHealth}/{maxHealth}";
        Debug.Log(coinNum);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateUi();
 
    }


    public void Test()
    {
        Debug.Log("DEBUG");
        
    }
}
