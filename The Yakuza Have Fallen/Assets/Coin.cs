using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
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
        playerBase.coinNum++;
        
        playerBase.UpdateUi();
        Destroy(gameObject);



    }
}
