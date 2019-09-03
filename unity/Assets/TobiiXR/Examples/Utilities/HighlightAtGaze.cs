// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using Tobii.G2OM;
using Tobii.XR;
using UnityEngine;

//Monobehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object receives focus
public class HighlightAtGaze : MonoBehaviour, IGazeFocusable
{
    public Color HighlightColor = Color.red;
    public float AnimationTime = 0.1f;

    public static bool isFocused;

    private Renderer _renderer;
    private Color _originalColor;
    private Color _targetColor;

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        isFocused = hasFocus;
        //If this object received focus, fade the object's color to highlight color
        if(hasFocus) 
        {
            if (TobiiXR.FocusedObjects.Count > 0)
            {
                if (TobiiXR.FocusedObjects[0].GameObject.tag == "selectableObj")
                {
                    SelectionManager.setWasAlreadySelected(true);
                    SelectionManager.isSelected = true;
                }
            }
            _targetColor = HighlightColor;
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
            if (!TobiiXR.EyeTrackingData.IsLeftEyeBlinking && !TobiiXR.EyeTrackingData.IsRightEyeBlinking)
            {
                SelectionManager.isSelected = false;
            }
            _targetColor = _originalColor;
        }
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _targetColor = _originalColor;
    }

    private void Update()
    {
        //This lerp will fade the color of the object
        _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / AnimationTime));
    }
}