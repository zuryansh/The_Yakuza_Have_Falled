using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    #region State Instances
    public IdleState IdleState;
    public PatrolState PatrolState;
    public AttackState AttackState;
    public ChaseState ChaseState;

    #endregion

    public State currentState;
    public NavMeshAgent agent;
    public Transform player;
    public bool canAttack;
    public bool canSeePlayer;
    public bool shouldPatrol;
    public Transform rayOrigin;
    public bool tookDamage;
    [HideInInspector]
    public EnemyBase enemyScript;
    public PlayerBase playerScript;

    private void Start()
    {
        enemyScript = GetComponent<EnemyBase>();
        player = FindObjectOfType<PlayerMovement>().transform;
        playerScript = player.GetComponent<PlayerBase>();
        agent = GetComponent<NavMeshAgent>();

        if (shouldPatrol)
            currentState = PatrolState;
        else
            currentState = IdleState;
        currentState?.EnterState(this);
    }

    void Update()
    {
        RunStateMachine();
        CanSeePlayer();
    }

    void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if(nextState != null && nextState != currentState)
        {
            SwitchToNextState(nextState);

        }
    }

    void SwitchToNextState(State nextState)
    {
        currentState = nextState;
        currentState.EnterState(this);
    }
    
    public void GotHit()
    {
        if(currentState == IdleState || currentState == PatrolState && gameObject!=null)
        {
            SwitchToNextState(IdleState);
            agent.SetDestination(player.position);
        }
    }

    void CanSeePlayer()
    {
        RaycastHit hit;
        if(Vector3.Distance(transform.position , player.position) < enemyScript.sightDistance)
        {
            if (Physics.Raycast(rayOrigin.position, (player.position - transform.position), out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    canSeePlayer = true;
                    CanAttackPlayer();

                    return;
                }       
            }
        }
        canSeePlayer = false;

    }

    void CanAttackPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < enemyScript.attackRange)
        {
            canAttack = true;
            return;
        }
        else
        {
            canAttack = false;
            return;
        }
    }

}
