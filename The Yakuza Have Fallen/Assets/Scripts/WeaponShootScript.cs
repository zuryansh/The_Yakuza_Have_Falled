using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class WeaponShootScript : MonoBehaviour
{
    //public string fireType;
    //public Transform firePoint;
    //public float range;
    public Camera fpsCam;
    //public int damage;
    public float originalFov;
    public float originalSensitivity;
    //public float zoomedFov;
    //public float zoomedSensitivity;
    AmmoSystem ammoScript;
    GunBase weapon;
    public int bulletsShot = 0;
    float originalSpread;
    Animator animator;
    GunEffects gunEffects;

  


    // Start is called before the first frame update
    void Start()
    {
        #region Component References
        animator = GetComponent<Animator>();
        originalSensitivity = fpsCam.GetComponent<MouseLook>().mouseSensitivity;
        ammoScript =GetComponent<AmmoSystem>();
        weapon = GetComponent<GunBase>();
        gunEffects = GetComponent<GunEffects>();
        #endregion

        originalFov = fpsCam.fieldOfView;
        originalSpread = weapon.spread; 
    }


    #region GunLogic

    public void SingleShot(float timeToWait)
    {
        if (weapon.readyToShoot)
        {
            CameraShaker.Instance.ShakeOnce(2.5f,4f,0.1f,0.4f);
            //animation
            animator.SetTrigger("Shoot");
            weapon.readyToShoot = false;
            //Get Spread
            Vector3 spread = ApplySpread(weapon.spread);
            //Send out Ray
            RaycastHit hit = SendOutRaycast(fpsCam.transform.position, fpsCam.transform.forward + spread, weapon.range);
            // subtract ammo
            if(ammoScript!= null)
                ammoScript.currentAmmo--;
            // if ray hits something 
            if (hit.transform != null)
            {
                //EFFECTS
                gunEffects.ShootEffects(hit);
                // damage enemy
                if (hit.transform.GetComponent<EnemyBase>())
                {
                    hit.transform.GetComponent<EnemyBase>().TakeDamage(weapon.damage);
                }
            }
            //reset the shot
            Invoke("ResetShot", timeToWait);
        }
    }

    public void AutomaticShot()
    {
        if (weapon.isFiring)
            SingleShot(weapon.timeBetweenShots); 
    }

    public void MultiShot()
    {
        if (weapon.readyToShootMultiple)
        {
            if (bulletsShot < weapon.shotsPerBullet)
            {
                SingleShot(0f);
                ResetShot();
                bulletsShot++;
                MultiShot();
            }
            weapon.readyToShootMultiple = false;
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.5f);
        }
        Invoke("ResetMultiShot", weapon.timeBetweenFires);
    }

    void ResetMultiShot()
    {
        weapon.readyToShootMultiple = true;
        bulletsShot = 0;
    }

    public static Vector3 ApplySpread(float spread)
    {
        float x = Random.Range(-spread,spread);
        float y = Random.Range(-spread, spread);

        return new Vector3(x, y, 0);
    }

    public static RaycastHit SendOutRaycast(Vector3 start , Vector3 direction, float range)
    {
        RaycastHit hit;
        Physics.Raycast(start, direction, out hit, range);
        return hit;           
    }

    public void ResetShot()
    {
        if (weapon.readyToShoot == false)
            weapon.readyToShoot = true;
    }

    public void ZoomIn()
    {
        fpsCam.fieldOfView = weapon.zoomedFov;
        weapon.spread = weapon.spread / 2;
        fpsCam.GetComponent<MouseLook>().mouseSensitivity = weapon.zoomedSensitivity;
    }

    public void ZoomOut()
    {
        
        weapon.spread = originalSpread;
        fpsCam.fieldOfView = originalFov;
        fpsCam.GetComponent<MouseLook>().mouseSensitivity = originalSensitivity;

    }
    #endregion

  
}
