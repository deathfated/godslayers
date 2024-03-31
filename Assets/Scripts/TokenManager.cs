using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    [System.Serializable]
    public struct BattleTokens
    {
        public string Name;
        public Token Type;
    }
    
    public BattleTokens[] battleTokens;
}
