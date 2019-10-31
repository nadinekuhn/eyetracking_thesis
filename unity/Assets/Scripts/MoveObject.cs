using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveObject : MonoBehaviour
{

    SpawnRandomObject_s3 obj;
    private float nextActionTime = 0.0f;
    public float period = 0.2f;
    private float t;
    float randomY = Random.Range(-15, 15);

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

        nextActionTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
       
        transform.Translate(obj.movementVector * obj.moveSpeed * Time.deltaTime);
        if (transform.position.x >= 80.0f)
        {
            obj.movementVector.Set(-obj.movementVector.x, obj.movementVector.y, obj.movementVector.z);
        }  else if (transform.position.x <= -80.0f)
        {
            obj.movementVector.Set(-obj.movementVector.x, obj.movementVector.y, obj.movementVector.z);
        }

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            print(nextActionTime + "\n" + Time.time);
            obj.movementVector = new Vector3(obj.movementVector.x, randomY, obj.movementVector.z);

            if (randomY > 0) {
                randomY = Random.Range(-15, 0);
            } else
            {
                randomY = Random.Range(0, 15);
            }
        }
        
    }
}
