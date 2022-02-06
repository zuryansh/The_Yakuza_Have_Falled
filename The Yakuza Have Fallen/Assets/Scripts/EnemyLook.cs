using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    public Transform body;
    public Transform gun;
    public GameObject Player;
    public GameObject playerCam;
    public GameObject enemyParent;
    StateManager stateScript;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;
        playerCam = FindObjectOfType<MouseLook>().gameObject;
        stateScript = GetComponentInParent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (stateScript.canSeePlayer)
        {
            Vector3 direction = body.position - Player.transform.position;
            Vector3 eulerRotation = Quaternion.LookRotation(direction).eulerAngles;
            transform.localRotation = Quaternion.Euler(eulerRotation.x, -0f, -0f);
            body.rotation = Quaternion.Euler(0f, eulerRotation.y, 0f);
        }
        else if (stateScript.currentState == stateScript.PatrolState || stateScript.currentState == stateScript.IdleState)
            if(stateScript.agent.velocity.magnitude>0)
                transform.rotation = Quaternion.LookRotation(-stateScript.agent.velocity.normalized);

        gun.forward = transform.forward;

    }
}
