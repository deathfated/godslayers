using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartyManager : MonoBehaviour
{
    public static EnemyPartyManager instance;

    [System.Serializable]
    public struct EnemyTokens
    {
        public string Name;
        //public bool isUnlocked;
        //public bool isInParty;
        public Token token;
    }
    
    public EnemyTokens[] eTokens; 

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetTokenSprite(int index)
    {
        Sprite sprit = eTokens[index].token.charaSprite;
        return sprit;
    }

    public string GetTokenName(int index)
    {
        return eTokens[index].Name;
    }

    public string GetTokenStat(int index, string stat)
    {
        string statToGet;
        switch (stat)
        {
            case("MaxHp"):
                statToGet = eTokens[index].token.maxHp.ToString();
                break;
            case("CurHp"):
                statToGet = eTokens[index].token.currHp.ToString();
                break;
            default:
                statToGet = "---";
                break;
        }
            
        return statToGet;

    }
}
