using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

    [CustomEditor(typeof(GunBase))]
public class GunBaseEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GunBase gunScript = (GunBase)target;     

        base.OnInspectorGUI();
            GUILayout.Space(10);
            EditorGUILayout.LabelField("STATS", EditorStyles.boldLabel);//ADDS HEADER
            gunScript.range = EditorGUILayout.FloatField("Range", gunScript.range);
            gunScript.damage = EditorGUILayout.IntField("Damage", gunScript.damage);
            gunScript.spread = EditorGUILayout.FloatField("Spread", gunScript.spread);
            gunScript.timeBetweenFires = EditorGUILayout.FloatField("Time Between Fires", gunScript.timeBetweenFires);
        if (gunScript.weaponType == GunBase.WeaponType.Automatic)
            gunScript.timeBetweenShots = EditorGUILayout.FloatField("Time Between Shots", gunScript.timeBetweenShots);
        if(gunScript.weaponType == GunBase.WeaponType.MultiShot)
            gunScript.shotsPerBullet = EditorGUILayout.IntField("Shots Per Bullet", gunScript.shotsPerBullet);
        
            GUILayout.Space(10);
            EditorGUILayout.LabelField("ZOOM OPTIONS", EditorStyles.boldLabel);//ADDS HEADER
            gunScript.zoomedFov = EditorGUILayout.FloatField("Zoomed Fov", gunScript.zoomedFov);
            gunScript.zoomedSensitivity = EditorGUILayout.FloatField("Zoomed Sensitivity", gunScript.zoomedSensitivity);

            GUILayout.Space(10);
            EditorGUILayout.LabelField("STATES", EditorStyles.boldLabel);//ADDS HEADER
        if (gunScript.weaponType == GunBase.WeaponType.Automatic || gunScript.weaponType == GunBase.WeaponType.MultiShot)
            gunScript.isFiring = EditorGUILayout.Toggle("is Firing", gunScript.isFiring);
        if (gunScript.weaponType == GunBase.WeaponType.MultiShot)
            gunScript.readyToShootMultiple = EditorGUILayout.Toggle("Ready To Shoot Multiple", gunScript.readyToShootMultiple);

        gunScript.readyToShoot = EditorGUILayout.Toggle("Ready To Shoot", gunScript.readyToShoot);

            GUILayout.Space(10);
            gunScript.firePoint = (Transform)EditorGUILayout.ObjectField("FirePoint",gunScript.firePoint , typeof(Transform) , true );






        

    }
}
