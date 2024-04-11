using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartyManager : MonoBehaviour
{
    public static PlayerPartyManager instance;

    [System.Serializable]
    public struct PlayerTokens
    {
        public string Name;
        public bool isUnlocked;
        public bool isInParty;
        public Token token;
    }
    
    public PlayerTokens[] pTokens; 

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetTokenSprite(int index)
    {
        Sprite sprit = pTokens[index].token.charaSprite;
        return sprit;
    }

    public string GetTokenName(int index)
    {
        return pTokens[index].Name;
    }

    public string GetTokenStat(int index, string stat)
    {
        string statToGet;
        switch (stat)
        {
            case("MaxHp"):
                statToGet = pTokens[index].token.maxHp.ToString();
                break;
            case("CurHp"):
                statToGet = pTokens[index].token.currHp.ToString();
                break;
            default:
                statToGet = "---";
                break;
        }
            
        return statToGet;

    }
}
