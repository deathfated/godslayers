using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;
    public string CurrTurn;
    public string PlayerState;


    void Start()
    {
        CurrTurn = "Player"; //Player, Enemy
        PlayerState = "Idle"; //Idle, Moving, CheckToken
        UpdateTurnText();
    }
    public void ChangeTurn(bool isFlipflop)
    {

        if(isFlipflop) //normal combat end turn: Player -> Enemy, vice versa
        {
            if (CurrTurn == "Player") CurrTurn = "Enemy";
            else if (CurrTurn == "Enemy") CurrTurn = "Player";

            UpdateTurnText();
        }
        else //unusual turn: ally turn, cinematic or something
        {
            
        }
        
    }

    void UpdateTurnText()
    {
        turnText.text = CurrTurn;
        //Debug.Log("Current State = " + CurrTurn);
    }

    void GameOver()
    {

    }
}
