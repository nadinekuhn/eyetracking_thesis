using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSelectableObject_s1 : SpawnSelectableObject
{
    private new Vector3 size = new Vector3(5.3f,5.3f,1);
    
    protected override void setPosition()
    {
        position = new[] {new Vector3(-3, 0.5f, 0),
                                        new Vector3(10, -4, 0),
                                        new Vector3(16, 6, 0),
                                        new Vector3(-22, -6, 0),
                                        new Vector3(20, -0.5f, 0),
                                        };
    }
}
