using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveObject : MonoBehaviour
{

    SpawnRandomObject_s3 obj;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "Szenario 3")
        {
            print("hello test");
            GetComponent<MoveObject>().enabled = false;
        }

        GameObject gameController = GameObject.FindGameObjectWithTag("randomObject");
        obj = gameController.GetComponent<SpawnRandomObject_s3>();
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(obj.movementVector * obj.moveSpeed * Time.deltaTime);
        if (transform.position.x >= 80.0f)
        {
            obj.movementVector = -obj.movementVector;
        }  else if (transform.position.x <= -80.0f)
        {
            obj.movementVector = -obj.movementVector;
        }
       
    }
}
