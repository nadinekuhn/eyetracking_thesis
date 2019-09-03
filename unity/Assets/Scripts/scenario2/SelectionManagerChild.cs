using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagerChild : SelectionManager
{
   
    protected override void actionWhenDestroyed()
    {
        SpawnSelectableObject.setSuccessfulSelection(true);
        interval = durationOfSelection;
        isDestroyed = false;
    }

}
