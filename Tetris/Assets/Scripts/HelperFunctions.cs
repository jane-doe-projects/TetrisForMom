using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions : MonoBehaviour
{
    public static GameObject FindInactiveGameObjectWithName(string name)
    {
        GameObject[] resources = Resources.FindObjectsOfTypeAll<GameObject>() as GameObject[];

        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].name == name)
                return resources[i];
        }

        return null;
    }

}
