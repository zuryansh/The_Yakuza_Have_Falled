using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedPickup : MonoBehaviour
{
    public float duration;
    public float multiplier;
    public PickupManager pickupManager;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.gameObject);
        }
    }

    void Pickup(GameObject player)
    {
        PlayerBase playerBase = player.GetComponent<PlayerBase>();

        //diabele
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        //effect
        if (playerBase.speedPickupNum < playerBase.maxSpeedNum)
        {
            player.GetComponent<PlayerMovement>().speed *= multiplier;
            playerBase.speedPickupNum++;
            //wait
            if (duration > 0)
                StartCoroutine(DisablePowerup(player));
            else
                Destroy(gameObject);

        }
    }

    IEnumerator DisablePowerup(GameObject player)
    {
        yield return new WaitForSeconds(duration);
        player.GetComponent<PlayerMovement>().speed /= multiplier;
        Destroy(gameObject);
    }

}
