using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    StateManager stateManager;
    GunBase weapon;
    Animator animator;
    public Transform rayStart;
    

    public override void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        
        weapon = GetComponentInChildren<GunBase>();
        animator = GetComponentInChildren<Animator>();
        
    }

    public override State RunCurrentState()
    {
        if (!stateManager.canAttack && stateManager.canSeePlayer)
        {
            //switch to chase state;
            return stateManager.ChaseState;
        }
        else if(!stateManager.canAttack && !stateManager.canSeePlayer)
        {
            //switch to patrol state
            if (stateManager.shouldPatrol)
            {
                return stateManager.PatrolState;
            }
            else
            {
                return stateManager.IdleState;
            }
        }
        else
        {
            //stay in current state and shoot
            Shoot(weapon.timeBetweenFires);
            return this;
        }

    }


    public void Shoot(float timeToWait)
    {
        if (weapon.readyToShoot)
        {

            //animation
            animator.SetTrigger("Shoot");
            weapon.readyToShoot = false;
            //Get Spread
            Vector3 spread = WeaponShootScript.ApplySpread(weapon.spread);
            //Send out Ray
            RaycastHit hit = WeaponShootScript.SendOutRaycast(rayStart.position, -(rayStart.forward + spread), weapon.range);
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Player"))
                {//damage player
                    Debug.DrawLine(rayStart.position, hit.transform.position, Color.red);
                    stateManager.playerScript.TakeDamage(weapon.damage);
                }
            }
            //reset the shot
            Invoke("ResetShot", timeToWait);
        }
    }

    public void ResetShot()
    {
        if (weapon.readyToShoot == false)
            weapon.readyToShoot = true;
    }

}
