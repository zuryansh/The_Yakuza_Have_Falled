using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{

    public Transform[] patrolPoints;
    public int currentPoint;
    StateManager stateManager;
    public float waitTime;
    public bool once;

    public override void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        stateManager.agent.stoppingDistance = 0;


    }

    public override State RunCurrentState()
    {
        
        if (stateManager.canSeePlayer )
        {   // change state
            return stateManager.ChaseState;
        }
        else
        {//dont change state
            Patrol();
            return this;
        } 
            
    }



    void Patrol()
    {
        if (!stateManager.agent.hasPath || stateManager.agent.remainingDistance > 0.5f)
        {
            stateManager.agent.destination = patrolPoints[currentPoint].position;
        }
        else
        {
           
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPoint + 1 < patrolPoints.Length)
        {
            currentPoint++;
        }
        else
        {
            currentPoint = 0;
        }
        stateManager.agent.ResetPath();
        once = false;
    }
}



