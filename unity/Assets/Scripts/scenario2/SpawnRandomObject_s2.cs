using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObject_s2 : SpawnObject
{
    protected override void SpawnObjects()
    {
        // Scenario 2: Dense environement
        Vector3 pos = transform.position + new Vector3(Random.Range(min * 2, max * 2), Random.Range(min, max * 0.7f), Random.Range(min / 2, max / 2));
        randomObjPrefab.transform.localScale = size;
                

        if(!Physics.CheckBox(pos, size))
        {
            Instantiate(randomObjPrefab, pos, Quaternion.identity, transform);
            objSpawned++;
        }
        
    }
   
}
