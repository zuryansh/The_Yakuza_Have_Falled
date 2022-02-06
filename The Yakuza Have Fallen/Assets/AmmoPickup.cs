using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
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
        AmmoSystem[] ammoSystems = player.GetComponentsInChildren<AmmoSystem>(true);

        foreach (AmmoSystem scrpit in ammoSystems)
        {
            scrpit.clipCount++;
        }
        Destroy(gameObject);

    }


}
