using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Dictionary<Vector2, Tile> _tilesDic;
    private int _rows;
    private int _collumns;

    private GameObject _gameMan;
    private TurnManager _turnMan;
    private ActionManager _actMan;

    [SerializeField] GameObject testTokenPlayer;
    [SerializeField] GameObject testTokenEnemy;
    [SerializeField] float tokenMoveSpeed = 10f;

    private Vector2 positi;

    void Start()
    {
        _rows = transform.GetChild(0).childCount;
        _collumns = transform.GetChild(0).GetChild(0).childCount;

        _tilesDic = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _collumns ; x++)
        {
            for (int y = 0; y < _rows ; y++)
            {
                Tile _grabbedTile = transform.GetChild(0).GetChild(y).GetChild(x).GetComponent<Tile>();

                //_tilesDic[new Vector2(x,y)] = _grabbedTile;
                _tilesDic.Add(new Vector2(x,y), _grabbedTile);
            }
        }

        _gameMan = GameObject.FindGameObjectWithTag("BattleManager");
        _turnMan = _gameMan.GetComponent<TurnManager>();
        _actMan = _gameMan.GetComponent<ActionManager>();
    }

    public Tile GetTileAtPos(Vector2 pos)
    {
        if(_tilesDic.TryGetValue(pos,out Tile tile))
        {
            return tile;
        }
        
        return null;
    }

    public void TilePressed(Vector2 posi)
    {
        
        if (_turnMan.CurrTurn != "Player") return;

        _actMan.ShowEnemyPanel(false);
        
        switch (_turnMan.PlayerState)
        {
            case ("Idle"):
                
                //check if click on player tile
                Vector2 tempPosi = testTokenPlayer.transform.position;
                tempPosi = new Vector2(-(tempPosi.y - 4), tempPosi.x + 8);
                
                if (posi == tempPosi)
                {
                    positi = posi;
                    _actMan.ShowPanel(true);
                }
                
                //check if click on enemy tile
                Vector2 tempEnemy = testTokenEnemy.transform.position;
                tempEnemy = new Vector2(-(tempEnemy.y - 4), tempEnemy.x + 8);

                if (posi == tempEnemy)
                {
                    //positi = posi;
                    _actMan.ShowEnemyPanel(true);
                }

                break;

            case ("Moving"):
                
                _actMan.ShowPanel(false);
                
                //check if tile is in range
                _tilesDic.TryGetValue(new Vector2(posi.y, posi.x), out Tile _Tile);

                if (_Tile.IsMoveable) MoveToken(posi);

                //reset moveable tiles
                for (int x = 0; x < _tilesDic.Count ; x++)
                {
                    for (int y = 0; y < _tilesDic.Count ; y++)
                    {
                        if (_tilesDic.TryGetValue(new Vector2(x, y), out Tile _tempTile))
                        {
                        _tempTile.SetTileMoveable(false);
                        }
                    }
                }

                //reset PlayerState
                _turnMan.PlayerState = "Idle";
                
                break;

            case ("Attacking"):

                _actMan.ShowPanel(false);

                //check if tile is in range
                _tilesDic.TryGetValue(new Vector2(posi.y, posi.x), out Tile _TileA);

                //check if click on enemy tile
                Vector2 tempPos = testTokenEnemy.transform.position;
                tempPos = new Vector2(-(tempPos.y - 4), tempPos.x + 8);

                if(_TileA.IsAttackable && tempPos == posi) 
                {
                    
                    //testTokenPlayer.GetComponent<PlayerToken>().
                    int dmg = 3;
                    testTokenEnemy.GetComponent<EnemyToken>().OnHpReduced(dmg);
                    //Debug.Log("Attacking for "+ dmg +" damage!");
                    
                }

                //reset attackable tiles
                for (int x = 0; x < _tilesDic.Count ; x++)
                {
                    for (int y = 0; y < _tilesDic.Count ; y++)
                    {
                        if (_tilesDic.TryGetValue(new Vector2(x, y), out Tile _tempTile))
                        {
                        _tempTile.SetTileAttackable(false);
                        }
                    }
                }

                //reset PlayerState
                _turnMan.PlayerState = "Idle";

                break;
        } 
    }

    public void CheckActionable(string actionType)
    {
        int possibleDistance = 1;// testTokenPlayer.GetComponent<PlayerToken>().MaxActionPoints;
        switch(actionType)
        {
            case ("move"):
                possibleDistance = 2;
                break;
            case ("attack"):
                possibleDistance = 1;
                break;
        }
        
        int possibleTargetX = Mathf.RoundToInt(positi.x); 
        int possibleTargetY = Mathf.RoundToInt(positi.y); 
        Debug.Log("posDis : " + possibleDistance + " , posX : " + possibleTargetX + " , posY : " + possibleTargetY);

        //check tile dictionary
        for (int x = -possibleDistance; x < possibleDistance + 1 ; x++)
        {
            for (int y = -possibleDistance; y < possibleDistance + 1 ; y++)
            {
                if (_tilesDic.TryGetValue(new Vector2( possibleTargetY + y, possibleTargetX + x), out Tile _tempTile))
                {
                    switch(actionType)
                    {
                        case ("move"):
                            _tempTile.SetHighlightColor(actionType);
                            if (!_tempTile.IsOccupied) _tempTile.SetTileMoveable(true);

                            _turnMan.PlayerState = "Moving";
                            break;
                        case ("attack"):
                            _tempTile.SetHighlightColor(actionType);
                            _tempTile.SetTileAttackable(true);

                            _turnMan.PlayerState = "Attacking";
                            break;
                    }
                    
                }
                
            }
        }

        //reset positi value
        positi = new Vector2(0,0);

    }

    private void MoveToken(Vector2 posit)
    {
        //Debug.Log("position is on : " + posi[0] + " , " + posi[1]);

        //test move token to new pos
        Vector3 targetPos = new Vector3((posit[1] - 8), (-posit[0] + 4), 0f);
        //testTokenPlayer.transform.position = targetPos; //object name is flipped with actual unity's x y 

        //smooth movin
        StartCoroutine(MoveTokenSmooth(targetPos));
    }

    IEnumerator MoveTokenSmooth(Vector3 targetpos)
    {
        while(testTokenPlayer.transform.position != targetpos)
            {
                testTokenPlayer.transform.position = Vector2.MoveTowards(
                    testTokenPlayer.transform.position,
                    targetpos,
                    tokenMoveSpeed * Time.deltaTime);
                yield return 0;
            }
            testTokenPlayer.transform.position = targetpos;

    }
}
