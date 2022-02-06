using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Start()
    {
        current = this;
    }
    public event Action<RaycastHit> OnGunFire;
    public void GunFire(RaycastHit hit)
    {
        if (OnGunFire != null)
        {
            OnGunFire.Invoke(hit);
        }
    }
}
