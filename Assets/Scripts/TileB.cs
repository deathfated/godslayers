using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private GameObject highlightObj;
    private bool _isPressed;

    void Start()
    {
        highlightObj = transform.GetChild(0).gameObject;
    }

    void HighlightSelf()
    {
        highlightObj.SetActive(true);
    }


    void OnMouseEnter()
    {
        HighlightSelf();
    }

    void OnMouseExit()
    {
        highlightObj.SetActive(false);
    }

    void OnMouseDown()
    {
        _isPressed = true;

        //call event to highlight possible movement
    }

    void OnMouseUp()
    {
        if (_isPressed)
        {
            //reset
            _isPressed = false;
        }
    }
}
