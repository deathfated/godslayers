using UnityEditor.Animations;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    
    [System.Serializable]
    public struct BattleTokens
    {
        public string Name;
        public Token Type;
    }
    
    public BattleTokens[] playerTokens;
    public BattleTokens[] enemyTokens;

    public Sprite GetTokenSprite(int index)
    {
        Sprite sprit = enemyTokens[index].Type.charaSprite;
        return sprit;
    }

    public string GetTokenName(int index)
    {
        return enemyTokens[index].Name;
    }

    public string GetTokenStat(int index, string stat)
    {
        string statToGet;
        switch (stat)
        {
            case("MaxHp"):
                statToGet = enemyTokens[index].Type.maxHp.ToString();
                break;
            case("CurHp"):
                statToGet = enemyTokens[index].Type.currHp.ToString();
                break;
            default:
                statToGet = "---";
                break;
        }
            
        return statToGet;

    }

    void Start()
    {
        for (int i = 0 ; i < playerTokens.Length ; i++)
        {
            playerTokens[i].Name = PlayerPartyManager.instance.pTokens[i].Name;
            playerTokens[i].Type.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerPartyManager.instance.pTokens[i].token.GetComponent<SpriteRenderer>().sprite;
            playerTokens[i].Type.MaxActionPoints = PlayerPartyManager.instance.pTokens[i].token.MaxActionPoints;
            playerTokens[i].Type.maxHp = PlayerPartyManager.instance.pTokens[i].token.maxHp;
            playerTokens[i].Type.currHp = playerTokens[i].Type.maxHp;
            playerTokens[i].Type.charaSprite = PlayerPartyManager.instance.pTokens[i].token.charaSprite;
            playerTokens[i].Type.animController = PlayerPartyManager.instance.pTokens[i].token.animController;

            playerTokens[i].Type.gameObject.GetComponent<Animator>().runtimeAnimatorController = playerTokens[i].Type.animController;
        }
        
    }
}
