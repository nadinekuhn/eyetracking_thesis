using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager_s1 : SelectionManager
{
    protected override void actionWhenDestroyed()
    {
        SpawnSelectableObject_s1.setSuccessfulSelection(true);
        interval = durationOfSelection;
        isDestroyed = false;
    }

}
