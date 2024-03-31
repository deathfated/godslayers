using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TokenManager : MonoBehaviour
{
    
    public static TokenManager instance;

    [System.Serializable]
    public struct BattleTokens
    {
        public string Name;
        public Token Type;
    }
    
    public BattleTokens[] battleTokens;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

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
        
        //GameObject.FindGameObjectWithTag("PlayerToken");
    }
}
