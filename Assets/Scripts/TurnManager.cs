using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] TextMeshPro turnText;
    public string CurrTurn;

    public void ChangeTurn(string turn)
    {
        CurrTurn = turn;

        switch(turn)
        {
            case "Dialog":
                //stuff
                break;
            case "Player":
                //ss
                break;
            case "Enemy":
                //ssa
                break;
            
            
        }
    }

    void UpdateText()
    {
        turnText.text = CurrTurn;
    }
}
