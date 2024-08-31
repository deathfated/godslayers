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
    private TokenManager _tokenMan;

    private PlayerToken _currentActivePlayer;

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
                _tilesDic.Add(new Vector2(x,y), _grabbedTile);
            }
        }

        _gameMan = GameObject.FindGameObjectWithTag("BattleManager");
        _turnMan = _gameMan.GetComponent<TurnManager>();
        _actMan = _gameMan.GetComponent<ActionManager>();
        _tokenMan = _gameMan.GetComponent<TokenManager>();

        ScanOccupiedTiles();
    }

    public void TilePressed(Vector2 posi)
    {
        
        ScanOccupiedTiles();

        if (_turnMan.CurrTurn != "Player") return;

        _actMan.ShowEnemyPanel(false);
        _turnMan.UpdateTurnText();
        
        switch (_turnMan.PlayerState)
        {
            case ("Idle"):
                
                //check if click on player tile
                for (int i = 0; i < _tokenMan.playerTokens.Length; i++)
                {
                    Vector2 tempPosi = _tokenMan.playerTokens[i].Type.gameObject.transform.position;
                    tempPosi = new Vector2(-(tempPosi.y - 4), tempPosi.x + 8);

                    if (posi == tempPosi && _tokenMan.playerTokens[i].Type.gameObject.activeSelf)
                    {
                        positi = posi;
                        _currentActivePlayer = (PlayerToken)_tokenMan.playerTokens[i].Type;
                        _actMan.ShowPanel(true, _currentActivePlayer);
                    }
                }
                
                //check if click on enemy tile
                for(int n = 0; n < _tokenMan.enemyTokens.Length; n++)
                {
                    Vector2 tempEnemy = _tokenMan.enemyTokens[n].Type.gameObject.transform.position;
                    tempEnemy = new Vector2(-(tempEnemy.y - 4), tempEnemy.x + 8);

                    if (posi == tempEnemy && _tokenMan.enemyTokens[n].Type.gameObject.activeSelf)
                    {
                        _actMan.ShowEnemyPanel(true);
                    }
                }

                break;

            case ("Moving"):
                
                _actMan.ShowPanel(false, _currentActivePlayer);
                
                //check if tile is in range
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
                }

                //check if target tile is on a damaging Misc                
                for(int t = 0; t < _tokenMan.miscTokens.Length; t++ )
                {
                    if (_tokenMan.miscTokens[t].isDamaging)
                    {
                        Vector2 tempObst = _tokenMan.miscTokens[t].transform.position;
                        tempObst = new Vector2(-(tempObst.y - 4), tempObst.x + 8);

                        if (posi == tempObst)
                        {
                            //do damage to player
                            _currentActivePlayer.OnHpReduced(_tokenMan.miscTokens[t].damage);

                        }
                    }
                }

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

                _actMan.ShowPanel(false, _currentActivePlayer);

                //check if tile is in range
                _tilesDic.TryGetValue(new Vector2(posi.y, posi.x), out Tile _TileA);

                //check if click on enemy tile
                for(int n = 0; n < _tokenMan.enemyTokens.Length; n++)
                {
                    Vector2 tempEnemy = _tokenMan.enemyTokens[n].Type.gameObject.transform.position;
                    tempEnemy = new Vector2(-(tempEnemy.y - 4), tempEnemy.x + 8);

                    if(_TileA.IsAttackable && tempEnemy == posi && _tokenMan.enemyTokens[n].Type.gameObject.activeSelf) 
                    {
                        //testTokenPlayer.GetComponent<PlayerToken>().
                        int dmg = 3;
                        _tokenMan.enemyTokens[n].Type.OnHpReduced(dmg);
                        Debug.Log(_tokenMan.enemyTokens[n].Name + " takes "+ dmg +" damage!");
                    }
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
        int possibleDistance;
        switch(actionType)
        {
            case ("move"):
                possibleDistance = _currentActivePlayer.MaxActionPoints; //2;
                break;
            case ("attack"):
                possibleDistance = 1; 
                break;
            default:
                possibleDistance = 0;
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

                        default:
                            Debug.LogError("action type value invalid!");
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

        StartCoroutine(MoveTokenSmooth(targetPos));
    }

    IEnumerator MoveTokenSmooth(Vector3 targetpos)
    {
        while(_currentActivePlayer.transform.position != targetpos)
            {
                _currentActivePlayer.transform.position = Vector2.MoveTowards(
                    _currentActivePlayer.transform.position,
                    targetpos,
                    tokenMoveSpeed * Time.deltaTime);
                yield return 0;
            }
            _currentActivePlayer.transform.position = targetpos;

    }

    public void ScanOccupiedTiles()
    {
        foreach(KeyValuePair<Vector2,Tile> entry in _tilesDic)
        {    
            entry.Value.GetPositionFromName(out int row, out int col);
            Vector2 posi = new Vector2(row, col);

            entry.Value.IsOccupied = false;
                
                for (int i = 0; i < _tokenMan.playerTokens.Length; i++)
                {
                    Vector2 tempPosi = _tokenMan.playerTokens[i].Type.gameObject.transform.position;
                    tempPosi = new Vector2(-(tempPosi.y - 4), tempPosi.x + 8);

                    if (posi == tempPosi && _tokenMan.playerTokens[i].Type.gameObject.activeSelf)
                    {
                        entry.Value.IsOccupied = true;
                    }
                }
                
                for(int n = 0; n < _tokenMan.enemyTokens.Length; n++)
                {
                    Vector2 tempEnemy = _tokenMan.enemyTokens[n].Type.gameObject.transform.position;
                    tempEnemy = new Vector2(-(tempEnemy.y - 4), tempEnemy.x + 8);

                    if (posi == tempEnemy && _tokenMan.enemyTokens[n].Type.gameObject.activeSelf)
                    {
                        entry.Value.IsOccupied = true;
                    }
                }

                for(int n = 0; n < _tokenMan.miscTokens.Length; n++)
                {
                    Vector2 tempMisc = _tokenMan.miscTokens[n].gameObject.transform.position;
                    tempMisc = new Vector2(-(tempMisc.y - 4), tempMisc.x + 8);

                    if (posi == tempMisc && _tokenMan.enemyTokens[n].Type.gameObject.activeSelf && _tokenMan.miscTokens[n].isOccupying)
                    {
                        entry.Value.IsOccupied = true;
                    }
                }
        }
    }
}
