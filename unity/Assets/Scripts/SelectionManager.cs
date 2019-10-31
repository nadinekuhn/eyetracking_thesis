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
    private static string startTime;
    private static string endTime;


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
            WriteLogfile.writeRealtimeFile(System.DateTime.Now.ToString());

            selectionTimer += Time.deltaTime;

            if (interval > 0)
            {
                interval -= Time.deltaTime;
            }
            else
            {
                isDestroyed = true;
                if (GameObject.FindGameObjectWithTag("selectableObj"))
                {
                    GameObject.FindGameObjectWithTag("selectableObj").SetActive(false);
                }
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
            print("errorCounter:" + errorCounter);
            print("errorTime:" + errorTime);
            errorCounter = 0;
            errorTime = 0;
            selectionTimer = 0;
            wasAlreadySelected = false;
            endTime = System.DateTime.Now.ToString();
            actionWhenDestroyed();
        }



        /*print("interval: " + interval + "; isDestroyed: " + isDestroyed);
        print("errorCounter: " + errorCounter + "; errorTime: " + errorTime%60);*/
    }

    protected virtual void actionWhenDestroyed()
    {

    }

    public static void setWasAlreadySelected(bool selected)
    {
        wasAlreadySelected = selected;
        startTime = System.DateTime.Now.ToString();
    }

    public static float getSelectionTimer()
    {
        return selectionTimer;
    }

    public static string getStartTime()
    {
        return startTime;
    }

    public static string getEndTime()
    {
        return endTime;
    }


    public static float getErrorTimer()
    {
        print("GET errorTime:" + errorTime);
        return errorTime;
    }

    public static int getErrorCount()
    {
        print("GET errorCounter:" + errorCounter);
        return errorCounter;
    }

    public void BubbleCursor()
    {
        allObjects = GameObject.FindGameObjectsWithTag("randomObject");
        print(allObjects.Length);
    }
}
