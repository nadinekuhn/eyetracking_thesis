using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTo2dHelper : MonoBehaviour
{
    public static Vector2 GUICoordinatesWithObject(GameObject go)
    {
        Vector3 pos = go.GetComponent<Renderer>().transform.position;
        return WorldToGUIPoint(pos);
    }

    public static Vector2 WorldToGUIPoint(Vector3 world)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(world);
        return screenPoint;


    }
}
