using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.gameObject);
        }
    }

    void Pickup(GameObject player)
    {
        //diabele
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        //effect
        PlayerBase playerBase = player.GetComponent<PlayerBase>();
        playerBase.currentHealth = playerBase.maxHealth;
        playerBase.UpdateUi();
        Destroy(gameObject);


    }
}
