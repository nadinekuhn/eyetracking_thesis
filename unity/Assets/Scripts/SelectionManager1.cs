using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager1 : MonoBehaviour
{
    [SerializeField ]private string selectableTag = "selectableObj";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    protected Transform _selection;

    // Timer for selection Trigger
    public float durationOfSelection;
    protected float interval;
    public static bool isDestroyed = false;
    protected bool wasAlreadySelected = false;

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
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
            
        } else if (_selection == null && wasAlreadySelected)
        {
            interval = durationOfSelection;
            errorTime += Time.deltaTime;
            if (isHit)
            {
                errorCounter++;
                isHit = false;
            }
        }
        
        RaycastHit hit;
        var ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            
            if (selection.CompareTag(selectableTag)) { 
                var selectionRenderer = selection.GetComponent<Renderer>();
                if(selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
                wasAlreadySelected = true;

                selectionRenderer.material.color = Color.Lerp(Color.yellow, Color.grey, interval);
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

        }

        if (_selection)
        {
            isHit = true;
            BubbleCursor();
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
