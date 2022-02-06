using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    public enum WeaponType
    {
        SingleShot , MultiShot , Automatic
    } 
    public enum Holder
    {
        Player,Enemy
    }
    public Holder Holder_is;
    public WeaponType weaponType;

    #region HiddenVariables
    [HideInInspector]
    public bool readyToShootMultiple;
    [HideInInspector]
    public Transform firePoint;
    [HideInInspector]
    public float range;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float zoomedFov;
    [HideInInspector]
    public float zoomedSensitivity;
    [HideInInspector]
    public bool isFiring;
    [HideInInspector]
    public float timeBetweenShots; // time between each bullet while holding down
    [HideInInspector]
    public float spread;
    [HideInInspector]
    public int shotsPerBullet;
    [HideInInspector]
    public float timeBetweenFires; // time between each fire of the gun 
    [HideInInspector]
    public bool readyToShoot;
    [HideInInspector]
    AmmoSystem ammoScript;

    #endregion

    Transform originalTransform;
    WeaponShootScript shootScript;
    



    private void Start()
    {
        
        ammoScript = GetComponent<AmmoSystem>();
        shootScript = GetComponent<WeaponShootScript>();
        
        if (weaponType == WeaponType.MultiShot)
        {
            readyToShootMultiple = true;
            readyToShoot = false;
        }
        else
        {
            readyToShoot = true;
            readyToShootMultiple = false;
        }
    }

    void Update()
    {

    #region Gun Fire Input
        if (Holder_is == Holder.Player)
        {

            if (ammoScript != null)
                if (ammoScript.isReloading || ammoScript.currentAmmo <= 0)
                    return;

            if (weaponType == WeaponType.SingleShot)
            {
                if (Input.GetButtonDown("Fire1"))
                    shootScript.SingleShot(timeBetweenFires);
            }

            else if (weaponType == WeaponType.Automatic)
            {
                isFiring = Input.GetButton("Fire1");
                if (isFiring && readyToShoot)
                    shootScript.AutomaticShot();
            }

            else if (weaponType == WeaponType.MultiShot)
            {
                if (Input.GetButtonDown("Fire1") && readyToShootMultiple)
                    shootScript.MultiShot();
            }


            if (Input.GetButtonDown("Fire2"))
                shootScript.ZoomIn();
            if (Input.GetButtonUp("Fire2"))
                shootScript.ZoomOut();
        }
    #endregion

    }

}

