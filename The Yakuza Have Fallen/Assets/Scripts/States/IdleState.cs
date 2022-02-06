using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State  
{
    StateManager stateManager;

    public override void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        

    }

    public override State RunCurrentState()
    {
        if (stateManager.canSeePlayer)
        {
            return stateManager.ChaseState;
        }
        else if (stateManager.shouldPatrol)
        {
            return stateManager.PatrolState;
        }
        else
        {
            return this;
        }
    }
}
