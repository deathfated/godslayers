using System.Collections;
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

            int AP = _tokenMan.enemyTokens[n].Type.MaxActionPoints;
            
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
                    closestPlayer = tempPosi;
                }
            }

            Debug.Log("Closest target for (" + _tokenMan.enemyTokens[n].Name + ") [" + tempEnemy + " is " + closestPlayer + "] (" + _tokenMan.playerTokens[closestIndex].Name + ")");

            //attacking, check if enemy in melee range
            if (Vector2.Distance(tempEnemy, closestPlayer) == 1)
            {
                for (int ap = 0; ap < AP; ap++)
                {
                    Debug.Log("Attackkkk");
                    AP -= 2;
                }
            }

            //move towards target
            if (Vector2.Distance(tempEnemy, closestPlayer) > 1) 
            {
                //use up AP points
                //for (int ap = 0; ap < AP; ap++)
                for (int ap = 0; ap < 1; ap++)
                {
                    Vector2 startPos = _tokenMan.enemyTokens[n].Type.gameObject.transform.position;
                    Debug.Log("distance player - enemy : " + (closestPlayer.x - tempEnemy.x) + " : " + (closestPlayer.y - tempEnemy.y));
                    
                    if ((closestPlayer.x - tempEnemy.x) > 0) 
                    {
                        //Tile _tile = _tileMan.GetTileAtPos(new Vector2(startPos.x + 1, startPos.y));
                        //if (_tile == null ) return;
                        //if (!_tile.IsMoveable) return;

                        //_tileMan.GetTileAtPos(startPos).IsOccupied = false;
                        //StartCoroutine(MoveTokenSmooth(new Vector2(startPos.x + 1, startPos.y), (EnemyToken)_tokenMan.enemyTokens[n].Type));
                        _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x - 1, startPos.y);
                        //_tile.IsOccupied = true;

                    }
                    else if ((closestPlayer.x - tempEnemy.x) < 0)
                    {
                        //Tile _tile = _tileMan.GetTileAtPos(new Vector2(startPos.x - 1, startPos.y));
                        //if (_tile == null ) return;
                        //if (!_tile.IsMoveable) return;

                        //_tileMan.GetTileAtPos(startPos).IsOccupied = false;
                        //StartCoroutine(MoveTokenSmooth(new Vector2(startPos.x - 1, startPos.y), (EnemyToken)_tokenMan.enemyTokens[n].Type));
                        _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x + 1, startPos.y);
                        //_tile.IsOccupied = true;

                    } 
                    else if ((closestPlayer.y - tempEnemy.y) > 0) 
                    {
                        //Tile _tile = _tileMan.GetTileAtPos(new Vector2(startPos.x, startPos.y - 1));
                        //if (_tile == null ) return;
                        //if (!_tile.IsMoveable) return;

                        //_tileMan.GetTileAtPos(startPos).IsOccupied = false;
                        //StartCoroutine(MoveTokenSmooth(new Vector2(startPos.x, startPos.y - 1), (EnemyToken)_tokenMan.enemyTokens[n].Type));
                        _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x, startPos.y - 1);
                        //_tile.IsOccupied = true;

                    }
                    else if ((closestPlayer.y - tempEnemy.y) < 0) 
                    {
                        //Tile _tile = _tileMan.GetTileAtPos(new Vector2(startPos.x, startPos.y + 1));
                        //if (_tile == null ) return;
                        //if (!_tile.IsMoveable) return;

                        //_tileMan.GetTileAtPos(startPos).IsOccupied = false;
                        //StartCoroutine(MoveTokenSmooth(new Vector2(startPos.x, startPos.y + 1), (EnemyToken)_tokenMan.enemyTokens[n].Type));
                        _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x, startPos.y + 1);
                        //_tile.IsOccupied = true;

                    }
                    else Debug.Log("Enemy Nowhere to move!");
                }
                //ap depleted
            }
            //enemy turn done
        }
        //all enemy turn done
    }

    IEnumerator MoveTokenSmooth(Vector3 targetpos, EnemyToken enemyToken)
    {
        targetpos = new Vector3((targetpos[1] - 8), (-targetpos[0] + 4), 0f);

        while(enemyToken.transform.position != targetpos)
            {
                enemyToken.transform.position = Vector2.MoveTowards(
                    enemyToken.transform.position,
                    targetpos,
                    10f * Time.deltaTime);
                yield return 0;
            }
            enemyToken.transform.position = targetpos;

    }
}
