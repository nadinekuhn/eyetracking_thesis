using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObject_s3 : SpawnObject
{
    public float moveSpeed = 0.5f;
    public Vector3 movementVector;

    protected override void SpawnObjects()
    {
        // Scenario 3: Dynamic movement
        Vector3 pos = transform.position + new Vector3(Random.Range(min * 2, max * 2), Random.Range(min, max * 0.7f), Random.Range(min / 2, max / 2));
        randomObjPrefab.transform.localScale = size;
                

        if(!Physics.CheckBox(pos, size))
        {
            GameObject obj = Instantiate(randomObjPrefab, pos, Quaternion.identity, transform);
            objSpawned++;
        }
        
    }
   
}
