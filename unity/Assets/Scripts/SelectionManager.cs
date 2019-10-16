using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.XR;

public class SelectionManager : MonoBehaviour
{
    public static bool isSelected = false;

    // Timer for selection Trigger
    public float durationOfSelection;
    protected float interval;
    public static bool isDestroyed = false;
    protected static bool wasAlreadySelected = false;

    // Data for logfile
    private bool isHit = false;
    private static int errorCounter = 0;
    private static float errorTime;
    private static float selectionTimer;

    // Selection Method
    public static string selectionMethod = "normal";
    GameObject[] allObjects;

    // Start is called before the first frame update
    void Start()
    {
        interval = durationOfSelection;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!isSelected && wasAlreadySelected)
        {
            interval = durationOfSelection;
            errorTime += Time.deltaTime;
            if (isHit)
            {
                errorCounter++;
                isHit = false;
            }
        }
       
        if (wasAlreadySelected)
        {
            selectionTimer += Time.deltaTime;

            if (interval > 0)
            {
                interval -= Time.deltaTime;
            }
            else
            {
                isDestroyed = true;
                GameObject.FindGameObjectWithTag("selectableObj").SetActive(false);
            }
        }
        

        if (isSelected)
        {
            isHit = true;
            //BubbleCursor();
        }

        if (isDestroyed)
        {
            WriteLogfile.writeToFile();
            errorCounter = 0;
            errorTime = 0;
            selectionTimer = 0;
            wasAlreadySelected = false;
            actionWhenDestroyed();
        }

        

        /*print("interval: " + interval + "; isDestroyed: " + isDestroyed);
        print("errorCounter: " + errorCounter + "; errorTime: " + errorTime%60);*/
    }

    protected virtual void actionWhenDestroyed()
    {
        
    }

    public static void setWasAlreadySelected(bool selected) {
        wasAlreadySelected = selected;
    }

    public static float getSelectionTimer()
    {
        return selectionTimer;
    }

    public static float getErrorTimer()
    {
        return errorTime;
    }

    public static int getErrorCount()
    {
        return errorCounter;
    }

    public void BubbleCursor()
    {
        allObjects = GameObject.FindGameObjectsWithTag("randomObject");
        print(allObjects.Length);
    }
}
