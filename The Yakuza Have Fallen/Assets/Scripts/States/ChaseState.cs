using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    StateManager stateManager;

    public override void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        stateManager.agent.stoppingDistance = stateManager.enemyScript.stoppingDistance;

    }

    public override State RunCurrentState()
    {
        
        if (!stateManager.canSeePlayer)
        {
            // switch to default state
            //stateManager.agent.ResetPath();
            if (stateManager.shouldPatrol)
            {
                return stateManager.PatrolState;
            }
            else
            {
                return stateManager.IdleState;
            }
        }
        else if(stateManager.canSeePlayer && stateManager.canAttack)
        {   //switch to attck state
            return stateManager.AttackState;
        }
        else
        {
            ChasePlayer();
            return this;
        }
        

    }




    void ChasePlayer()
    {
        Vector3 destination = stateManager.player.position;
        if(stateManager.agent.destination != destination)
            stateManager.agent.SetDestination(destination);                 
    }


}
