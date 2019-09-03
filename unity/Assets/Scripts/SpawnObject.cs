using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject randomObjPrefab;

    public Vector3 size;

    public int numberOfObj;
    private static int objectCount;

    protected int objSpawned;

    public float min, max;

    public void Start()
    {
        randomObjPrefab.layer = 9;
        objSpawned = 0;
        SpawnObjects();
        objectCount = numberOfObj;
    }

    public void Update()
    {
        if (objSpawned < numberOfObj)
        {
            SpawnObjects();
        }
    }

    protected virtual void SpawnObjects()
    {
        // Inherit        
    }

    public static int getObjectCount()
    {
        return objectCount;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
