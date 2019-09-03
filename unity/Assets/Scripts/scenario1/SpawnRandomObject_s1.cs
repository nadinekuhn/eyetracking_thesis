using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObject_s1 : SpawnObject
{

    protected override void SpawnObjects()
    {
        // Scenario 1: Different sizes
        Vector3 pos = transform.position + new Vector3(Random.Range(min*2, max*2), Random.Range(min, max), 0);
        float randomSize = Random.Range(0.5f, 3.5f);
        size = new Vector3(randomSize, randomSize, 2);
        randomObjPrefab.transform.localScale = size;
            

        if(!Physics.CheckBox(pos, size))
        {
            Instantiate(randomObjPrefab, pos, Quaternion.identity, transform);
            objSpawned++;
        }
        
    }
   
}
