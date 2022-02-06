using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void EnterState(StateManager stateManager);

    public abstract State RunCurrentState();

}

