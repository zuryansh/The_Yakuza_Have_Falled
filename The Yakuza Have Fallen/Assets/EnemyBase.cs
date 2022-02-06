using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public float attackRange;
    public float stoppingDistance;
    public float sightDistance;
    StateManager stateManager;
    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponent<StateManager>();
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        stateManager.GotHit();
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
