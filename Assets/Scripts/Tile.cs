using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    private GameObject highlightAction;
    
    private Color moveColor, attackColor;
    
    private bool _isPressed;
    public bool IsMoveable;
    public bool IsAttackable;
    public bool IsOccupied;


    void Start()
    {
        highlightObj = transform.GetChild(0).gameObject;
        highlightAction = transform.GetChild(1).gameObject;

        moveColor = new Color(0, 0, 1, 0.5f);
        attackColor = new Color(1, 0, 0, 0.5f);

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
        highlightAction.SetActive(isActive);
        IsMoveable = isActive;
    }

    public void SetTileAttackable(bool isActive)
    {
        highlightAction.SetActive(isActive);
        IsAttackable = isActive;
    }

    public void SetHighlightColor(string actionType)
    {
        switch(actionType)
        {
            case ("move"):
                highlightAction.GetComponent<SpriteRenderer>().color = moveColor;
                break;
            case ("attack"):
                highlightAction.GetComponent<SpriteRenderer>().color = attackColor;
                break;
        }
    }

}
