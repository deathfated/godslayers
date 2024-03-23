using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//public struct TileData
//{
//    Vector2 position;
//    GameObject occupier;
//}

public class Tile : MonoBehaviour
{

    [Serializable]
    public class TilePressed : UnityEvent <Vector2>{}
    public TilePressed tilePressed; 

    private GameObject highlightObj;
    private GameObject highlightMove;
    private bool _isPressed;
    private bool _isMoveable;

    void Start()
    {
        highlightObj = transform.GetChild(0).gameObject;
        highlightMove = transform.GetChild(1).gameObject;

        tilePressed.AddListener(gameObject.GetComponentInParent<TileManager>().TilePressed);
    }

    void OnMouseEnter()
    {
        highlightObj.SetActive(true);
    }

    void OnMouseExit()
    {
        highlightObj.SetActive(false);
    }

    void OnMouseDown()
    {
        _isPressed = true;
        Debug.Log(this.name + " is now pressed");

        //getting the x y values from the object name
        string tempstring;
        string[] tempArr;
        Vector2 tempPos;

        int row, col;

        if (this.name.Contains("(")) 
        {
            tempstring = this.name.Replace("Tile","").Replace("(",",").Replace(")","").Replace(" ","");
            tempArr = tempstring.Split(",");

            int.TryParse(tempArr[0], out row);
            int.TryParse(tempArr[1], out col);

        }
        else //is the first tile in the Row so its number "0"
        {
            tempstring = this.name.Replace("Tile","popo,").Replace(" ","");
            tempArr = tempstring.Split(",");
            
            int.TryParse(tempArr[1], out row);
            col = 0;

        }

        //for (int x = 0; x < tempArr.Length ; x++)  //checking each split in array
        //{
        //    Debug.Log("temp Arr " + x + " : " + tempArr[x]);
        //}

        //Debug.Log("Current Tile pos : row " + row + " : column " + col );   //checking row and col integers

        tempPos = new Vector2(row,col);
        tilePressed.Invoke(tempPos);

        //call event to highlight possible movement


    }

    void OnMouseUp()
    {
        if (_isPressed)
        {
            _isPressed = false;
            //Debug.Log(this.name + " is no longer pressed");

        }
    }

    public void SetTileMoveable(bool isActive)
    {
        highlightMove.SetActive(isActive);
        _isMoveable = isActive;
    }

}
