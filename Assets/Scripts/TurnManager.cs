using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] EnemyTactics enemyTactics;
    public string CurrTurn;
    public string PlayerState;
    private TileManager _tileMan;


    void Start()
    {
        _tileMan = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        //enemyTactics.enemyTurnFinished() += ChangeTurn;

        CurrTurn = "Player"; //Player, Enemy
        PlayerState = "Idle"; //Idle, Moving, CheckToken, Attacking
        UpdateTurnText();
    }

    public void ChangeTurn(bool isFlipflop)
    {

        if(isFlipflop) //normal combat end turn: Player -> Enemy, vice versa
        {
            if (CurrTurn == "Player")
            { 
                CurrTurn = "Enemy";
                enemyTactics.TestEnemyTactics();
            }
            else if (CurrTurn == "Enemy")
            {
                CurrTurn = "Player";
            }
            UpdateTurnText();
        }
        else //unusual turn: ally turn, cinematic or something
        {
            
        }

        _tileMan.ScanOccupiedTiles();
    }

    public void UpdateTurnText()
    {
        turnText.text = CurrTurn;
        stateText.text = PlayerState;
    }

    void GameOver()
    {

    }
}
