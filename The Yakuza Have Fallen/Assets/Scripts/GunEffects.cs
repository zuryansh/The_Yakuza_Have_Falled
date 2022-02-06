using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffects : MonoBehaviour
{
   public GameObject[] bulletHoles;

   public void ShootEffects(RaycastHit hit)
    {


        //BULLET HOLE
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.isStatic)
            {
                GameObject _bulletHole = bulletHoles[Random.Range(0, bulletHoles.Length)];
                GameObject bulletHoleGO = Instantiate(_bulletHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHoleGO, 5f);
            }
        }
    }

}
