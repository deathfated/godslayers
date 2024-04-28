using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTactics : MonoBehaviour
{
    [SerializeField] TokenManager _tokenMan;
    [SerializeField] TileManager _tileMan;
    private int closestIndex = 999;

    public void TestEnemyTactics()
    {
        //do tactics for every enemy
        for(int n = 0; n < _tokenMan.enemyTokens.Length; n++)
        {
            //return if already ded
            if (_tokenMan.enemyTokens[n].Type.gameObject.activeSelf == false) return;
            
            Vector2 tempEnemy = _tokenMan.enemyTokens[n].Type.gameObject.transform.position;
            tempEnemy = new Vector2(-(tempEnemy.y - 4), tempEnemy.x + 8);

            //compare enemy pos with all player pos, pick closest player 
            Vector2 closestPlayer = new Vector2(999, 999);
            
            for (int i = 0; i < _tokenMan.playerTokens.Length; i++)
            {
                Vector2 tempPosi = _tokenMan.playerTokens[i].Type.gameObject.transform.position;
                tempPosi = new Vector2(-(tempPosi.y - 4), tempPosi.x + 8);

                if (Vector2.Distance(tempEnemy, tempPosi) < Vector2.Distance(tempEnemy, closestPlayer))
                {
                    closestIndex = i;
                    closestPlayer = _tokenMan.playerTokens[closestIndex].Type.gameObject.transform.position;
                }
            }

            //check if enemy in melee range
            Debug.Log("Closest target for (" + _tokenMan.enemyTokens[n].Name + ") is index " + closestIndex + "(" + _tokenMan.playerTokens[closestIndex].Name + ")");

            if (closestIndex == 1)
            {
                Debug.Log("Attackkkk");
            }
            //move towards target
            else 
            {
                /*//check if tile is in range
                _tilesDic.TryGetValue(new Vector2(posi.y, posi.x), out Tile _Tile);
                if (_Tile.IsMoveable) 
                {
                    //reset tile occupation, move, then set new occupation
                    _tilesDic.TryGetValue(new Vector2(_currentActivePlayer.transform.position.x + 8,
                                            -(_currentActivePlayer.transform.position.y - 4)), out Tile _lastTile);

                    _lastTile.IsOccupied = false;
                    MoveToken(posi);
                    _Tile.IsOccupied = true;

                    //_lastTile.gameObject.SetActive(false); -> kinda fun potential mechanic.. collapsing tiles when move away
                }*/
            }
        }
    }

    private void MoveTo()
    {

    }
}
