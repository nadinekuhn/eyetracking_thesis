using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnSelectableObject : MonoBehaviour
{
    public GameObject selectableObjPrefab;
    private GameObject instObj;

    protected static bool successfulSelection;
    protected static int numberOfSelection;
    public Vector3 size;
    public static bool isScenarioDone = false;

    protected Vector3[] position = new[] {new Vector3(3, -4, -5),
                                        new Vector3(-3, 0.5f, -10),
                                        new Vector3(-22, 6, -12.2f),
                                        new Vector3(-11, -6, 15),
                                        new Vector3(24, -0.5f, 0),
                                        };

    [SerializeField] private float min, max;

    // Start is called before the first frame update
    void Start()
    {
        selectableObjPrefab.layer = 9;
        successfulSelection = false;
        numberOfSelection = 0;
        SpawnObject(numberOfSelection);
        numberOfSelection++;
    }

    // Update is called once per frame
    void Update()
    {

        if (successfulSelection && numberOfSelection <= 4)
        {
            if (SceneManager.GetActiveScene().name == "Szenario 1")
            {
                size -= new Vector3(1.0f, 1.0f, 0);
            }
            SpawnObject(numberOfSelection);
            successfulSelection = false;
            numberOfSelection++;
            print("spawn");
        }

        if (successfulSelection && numberOfSelection == 5)
        {
            isScenarioDone = true;
            Destroy(GameObject.FindGameObjectWithTag("randomObject"));
        }
    }


    protected void SpawnObject(int vec)
    {
        
       Vector3 pos = transform.position + position[vec];

       selectableObjPrefab.transform.localScale = size;
       
       Instantiate(selectableObjPrefab, pos, Quaternion.identity, transform);

    }

    protected virtual void setPosition(){}

    public static void setSuccessfulSelection(bool b)
    {
        successfulSelection = b;
    }

    public static int getNumberOfSelection()
    {
        return numberOfSelection;
    }

    public static bool getSuccessfulSelection()
    {
        return successfulSelection;
    }
}
