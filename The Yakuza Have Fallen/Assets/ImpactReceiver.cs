using UnityEngine;

using System.Collections.Generic;

public class ImpactReceiver : MonoBehaviour
{
    static Dictionary<GameObject, List<Vector3>> forcesOnGameObjects = new Dictionary<GameObject, List<Vector3>>();

    // Update is called once per frame
    void Update()
    {
        List<GameObject> gameObjectsToRemove = new List<GameObject>();

        
        foreach (KeyValuePair<GameObject, List<Vector3>> gameObjectToBeMoved in forcesOnGameObjects)
        {
            GameObject target = gameObjectToBeMoved.Key;

            if (target == null)
            {
                gameObjectsToRemove.Add(gameObjectToBeMoved.Key);
                continue;
            }

            List<Vector3> impacts = gameObjectToBeMoved.Value;
            Vector3 finalImpact = Vector3.zero;
            foreach (Vector3 impact in impacts) finalImpact += impact;


            finalImpact = finalImpact.x * transform.right + transform.forward * finalImpact.z;
            target.GetComponent<CharacterController>().Move(finalImpact * Time.deltaTime);

            for (int i = 0; i < impacts.Count; i++)
            {
                forcesOnGameObjects[gameObjectToBeMoved.Key][i] = Vector3.Lerp(impacts[i], Vector3.zero, 5 * Time.deltaTime);
            }
        }
        foreach (GameObject gameObjectToRemove in gameObjectsToRemove)
        {
            forcesOnGameObjects.Remove(gameObject);
        }
    }

    public static void AddImpactOnGameObject(GameObject target, Vector3 impact)
    {
        // add object with impact to tuple
        if (!forcesOnGameObjects.ContainsKey(target))
        {
            List<Vector3> existingImpacts = new List<Vector3>();
            existingImpacts.Add(impact);
            forcesOnGameObjects.Add(target, existingImpacts);
        }
        else
        {
            List<Vector3> existingImpacts = forcesOnGameObjects[target];
            existingImpacts.Add(impact);
            forcesOnGameObjects[target] = existingImpacts;
        }
    }
}