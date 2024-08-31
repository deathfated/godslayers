using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [Serializable]
    public class TilePressed : UnityEvent <Vector2>{}
    public TilePressed tilePressed; 

    private GameObject highlightObj;
    private GameObject highlightAction;
    private GameObject highlightOccupied;
    
    private Color moveColor, attackColor;
    
    private bool _isPressed;
    public bool IsMoveable;
    public bool IsAttackable;
    public bool IsOccupied;

    void Start()
    {
        highlightObj = transform.GetChild(0).gameObject;
        highlightAction = transform.GetChild(1).gameObject;
        highlightOccupied = transform.GetChild(2).gameObject;

        moveColor = new Color(0, 0, 1, 0.5f);
        attackColor = new Color(1, 0, 0, 0.5f);

        tilePressed.AddListener(gameObject.GetComponentInParent<TileManager>().TilePressed);
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

    public void GetPositionFromName(out int row, out int col)
    {
        string tempstring;
        string[] tempArr;

        if (name.Contains("(")) 
        {
            tempstring = name.Replace("Tile","").Replace("(",",").Replace(")","").Replace(" ","");
            tempArr = tempstring.Split(",");

            int.TryParse(tempArr[0], out row);
            int.TryParse(tempArr[1], out col);
        }
        else //is the first tile in the Row thus its number is "0"
        {
            tempstring = name.Replace("Tile","popo,").Replace(" ","");
            tempArr = tempstring.Split(",");
            
            int.TryParse(tempArr[1], out row);
            col = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        IsOccupied = !IsOccupied;
        Debug.Log(this.name + " is now pressed");

        //getting the x y values from the object name
        GetPositionFromName(out int row, out int col);

        Vector2 tempPos = new Vector2(row,col);
        tilePressed.Invoke(tempPos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isPressed)
        {
            _isPressed = false;
            //Debug.Log(this.name + " is no longer pressed");

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightObj.SetActive(false);
    }

    void Update()
    {
        if (IsOccupied) highlightOccupied.SetActive(true);
        else highlightOccupied.SetActive(false);
    }
}
