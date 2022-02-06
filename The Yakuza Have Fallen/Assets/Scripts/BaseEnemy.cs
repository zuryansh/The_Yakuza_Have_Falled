using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseEnemy : MonoBehaviour
{
    public float health = 50;
    public TextMeshProUGUI healthText;

    

    private void Update()
    {
        healthText.text = health.ToString();
    }

    public void TakeDamage(float Damage)
    {
        
        health -= Damage;
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }
}
