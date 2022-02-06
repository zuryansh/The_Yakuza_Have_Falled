using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon;
    public int previousSelectedWeapon;
    public Camera fpsCam;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        previousSelectedWeapon = selectedWeapon;
        
        if (CheckIfCanSwitch())
        {
            GetMouseInput();
            
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
 
            DisableWeaponShooting();
            SelectWeapon();
            
        }
            
        
        

    }

    bool CheckIfCanSwitch()
    {

        if (Input.GetButton("Fire1"))
            return false;

        if (transform.GetChild(selectedWeapon).GetComponent<GunBase>() != null)
        {
            if (transform.GetChild(selectedWeapon).GetComponent<GunBase>().weaponType == GunBase.WeaponType.MultiShot && transform.GetChild(selectedWeapon).GetComponent<GunBase>().readyToShootMultiple == false)
            {
                return false;
            }
            else
            {
                if (transform.GetChild(selectedWeapon).GetComponent<GunBase>().readyToShoot)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (transform.GetChild(selectedWeapon).GetComponentInChildren< melleWeaponScript >() != null)
        {
            if (transform.GetChild(selectedWeapon).GetComponentInChildren<melleWeaponScript>().isSwinging)
            {
                return false;
            }
            
        }

            
        return true;
        
    }

    void GetMouseInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
    }

    void GetNumberInput()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            selectedWeapon = 2;
    }

    void DisableWeaponShooting()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i != selectedWeapon && weapon.GetComponent<GunBase>())
            {
                weapon.GetComponent<GunBase>().isFiring = false;
                //weapon.GetComponent<Animator>().SetTrigger("Idle");
                //weapon.GetComponent<GunBase>().readyToShoot = true;
                if  (fpsCam.fieldOfView!=50)
                    weapon.GetComponent<WeaponShootScript>().ZoomOut();
            }
           
            //else
            //    weapon.GetComponent<GunBase>().isFiring = false;
            i++;
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        
        
        foreach (Transform _weapon in transform)
        {

            //if (_weapon.GetComponent<GunBase>() != null)
            //    if (!(_weapon.GetComponent<GunBase>().readyToShoot || _weapon.GetComponent<GunBase>().readyToShootMultiple))//if wepaon is in attack animation
            //        return;

            if (i == selectedWeapon)
            {
                _weapon.gameObject.SetActive(true);
            }
            else
            {   
                _weapon.gameObject.SetActive(false);
            }

            //if (_weapon.GetComponent<WeaponShootScript>())
            //    if (fpsCam.fieldOfView != _weapon.GetComponent<WeaponShootScript>().originalFov)
            //        _weapon.GetComponent<WeaponShootScript>().ZoomOut();

            i++;
        }
    }
}
