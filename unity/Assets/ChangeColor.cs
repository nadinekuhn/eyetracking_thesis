using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    RectTransform rt;

    void Start()
    {
        rt = GetComponent(typeof(RectTransform)) as RectTransform;
        print(rt.name);
    }

    void Update()
    {
        rt.sizeDelta = new Vector2(15.0f,15.0f);
    }
    
}
