using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistantObjects = new GameObject[3];
    public int objectIndex;
    void Awake()
    {
        if (persistantObjects[objectIndex] == null)
        {
            persistantObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistantObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }

    }

}
