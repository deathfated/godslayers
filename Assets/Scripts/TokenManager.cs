using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TokenManager : MonoBehaviour
{
    
    [System.Serializable]
    public struct BattleTokens
    {
        public string Name;
        public Token Type;
    }
    
    public BattleTokens[] spawnTokens;
    public BattleTokens[] battleTokens;

    public Sprite GetTokenSprite(int index)
    {
        Sprite sprit = battleTokens[index].Type.charaSprite;
        return sprit;
    }

    public string GetTokenName(int index)
    {
        return battleTokens[index].Name;
    }

    public string GetTokenStat(int index, string stat)
    {
        string statToGet;
        switch (stat)
        {
            case("MaxHp"):
                statToGet = battleTokens[index].Type.maxHp.ToString();
                break;
            case("CurHp"):
                statToGet = battleTokens[index].Type.currHp.ToString();
                break;
            default:
                statToGet = "---";
                break;
        }
            
        return statToGet;

    }

    void Start()
    {
        for (int i = 0 ; i < spawnTokens.Length ; i++)
        {
            spawnTokens[i].Name = PlayerPartyManager.instance.pTokens[i].Name;
            spawnTokens[i].Type.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerPartyManager.instance.pTokens[i].token.GetComponent<SpriteRenderer>().sprite;
            spawnTokens[i].Type.MaxActionPoints = PlayerPartyManager.instance.pTokens[i].token.MaxActionPoints;
            spawnTokens[i].Type.maxHp = PlayerPartyManager.instance.pTokens[i].token.maxHp;
            spawnTokens[i].Type.currHp = spawnTokens[i].Type.maxHp;
            spawnTokens[i].Type.charaSprite = PlayerPartyManager.instance.pTokens[i].token.charaSprite;
        }
        
        //GameObject.FindGameObjectWithTag("PlayerToken");
    }
}
