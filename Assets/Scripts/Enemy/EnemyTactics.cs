using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTactics : MonoBehaviour
{
    [Serializable]
    public class EnemyTurnFinished : UnityEvent{}
    public EnemyTurnFinished enemyTurnFinished; 

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
            
            StartCoroutine(DoAction(n));
            
            //ap depleted, enemy turn done
        }
        //all enemy turn done
        enemyTurnFinished.Invoke();
    }

    IEnumerator DoAction(int n)
    {
        int AP = _tokenMan.enemyTokens[n].Type.MaxActionPoints;
        for (int ap = AP; ap > 0; ap--)
            {
                
                //update enemy position
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
                Debug.Log("Closest target for (" + _tokenMan.enemyTokens[n].Name + ") [" + tempEnemy + 
                        " is (" + _tokenMan.playerTokens[closestIndex].Name + ") [" + closestPlayer + "]");
                
                
                //attacking, check if enemy in melee range
                if (Vector2.Distance(tempEnemy, closestPlayer) == 1 && ap >= 2)
                {
                    DoAttack();
                }
                //move towards target
                else if (Vector2.Distance(tempEnemy, closestPlayer) > 1) 
                {
                    DoMove(closestPlayer, tempEnemy, n);
                }

                yield return new WaitForSeconds(1f);
            }
    }

    private void DoAttack()
    {
        Debug.Log("Attackkkk");
    }

    private void DoMove(Vector2 closestPlayer, Vector2 tempEnemy, int n)
    {

        Vector2 startPos = _tokenMan.enemyTokens[n].Type.gameObject.transform.position;
        Debug.Log("distance player - enemy : " + (closestPlayer.x - tempEnemy.x) + " : " + (closestPlayer.y - tempEnemy.y));
                    
        if ((closestPlayer.x - tempEnemy.x) > 0) 
        {
            //Tile _tile = _tileMan.GetTileAtPos(new Vector2(startPos.x + 1, startPos.y));
            //if (_tile == null ) return;
            //if (!_tile.IsMoveable) return;

            //tempPosi = new Vector2(-(tempPosi.y - 4), tempPosi.x + 8);

            //StartCoroutine(MoveTokenSmooth(new Vector2(startPos.x + 1, startPos.y), (EnemyToken)_tokenMan.enemyTokens[n].Type));
            _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x, startPos.y - 1);
            Debug.Log("1");

        }
        else if ((closestPlayer.x - tempEnemy.x) < 0)
        {
            _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x, startPos.y + 1);
            Debug.Log("2");
        } 
        else if ((closestPlayer.y - tempEnemy.y) > 0) 
        {
            _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x + 1, startPos.y);
            Debug.Log("3");
        }
        else if ((closestPlayer.y - tempEnemy.y) < 0) 
        {
            _tokenMan.enemyTokens[n].Type.gameObject.transform.position = new Vector2(startPos.x - 1, startPos.y);
            Debug.Log("4");
        }
        else Debug.Log("Enemy Nowhere to move!");

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
