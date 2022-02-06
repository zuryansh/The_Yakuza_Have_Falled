using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [Header("GROUND POUND")]
    public float hangTime;
    public float gravityMultiplier;
    public bool isGroundPounding;
    public float minimumDistance;
    public LayerMask groundMask;
    [Header("DASH")]
    public float dashMultiplier;
    public Transform fpsCam;
    public float dashTime;
    public float dashDistance;

    CharacterController controller;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
        
    }

    #region Ground Pound
    bool CanDoGroundPound()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit,200,groundMask))
        {
            if (hit.distance > minimumDistance)
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator GroundPound()
    {
        if (CanDoGroundPound())
        {
            isGroundPounding = true;
            //stop player
            playerMovement.enabled = false;
            //wait for hangtime
            yield return new WaitForSeconds(hangTime);
            playerMovement.enabled = true;
            //drop player hard
            playerMovement.gravity *= gravityMultiplier;
        }
    }

    public void ResetGroundPound()
    {
        playerMovement.gravity /= gravityMultiplier;
        isGroundPounding = false;
    }
    #endregion

    public IEnumerator Dash()
    {
        print("START");
        //start dash
        playerMovement.speed *= dashMultiplier;
        //wait
        yield return new WaitForSeconds(dashTime);
        //end dash
        playerMovement.speed /= dashMultiplier;
        print("DONE");
    }

}
