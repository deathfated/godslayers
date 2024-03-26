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
    //[SerializeField] float tokenMoveSpeed = 10f;

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
        
        switch (_turnMan.PlayerState)
        {
            case ("Idle"):
                
                //check if click on player tile
                Vector2 tempPosi = testTokenPlayer.transform.position;
                tempPosi = new Vector2(-(tempPosi.y - 4), tempPosi.x + 8);

                //Debug.Log("posi, testpos = " + posi + ", " + tempPosi + "||" + (posi == tempPosi));
                
                if (posi == tempPosi)
                {
                    positi = posi;
                    _actMan.ShowPanel(true);
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
        } 
    }

    public void CheckMoveable()
    {
        int possibleDistance = 2;// testTokenPlayer.GetComponent<PlayerToken>().MaxActionPoints;
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
                    if (!_tempTile.IsOccupied) _tempTile.SetTileMoveable(true);
                }
                
            }
        }

        //on with next state and reset positi value
        _turnMan.PlayerState = "Moving";
        positi = new Vector2(0,0);

    }

    private void MoveToken(Vector2 posit)
    {
        //Debug.Log("position is on : " + posi[0] + " , " + posi[1]);

        //test move token to new pos
        Vector3 targetPos = new Vector3((posit[1] - 8), (-posit[0] + 4), 0f);
        testTokenPlayer.transform.position = targetPos; //object name is flipped with actual unity's x y 

        //smooth movin
        /*moveTarget = new Vector3((posi[1] - 8), (-posi[0] + 4), 0f);
        //StartCoroutine(MoveToken());*/
    }

    /*IEnumerator MoveToken()
    {
        while(testTokenPlayer.transform.position != moveTarget)
            {
                testTokenPlayer.transform.position = Vector2.MoveTowards(
                    testTokenPlayer.transform.position,
                    moveTarget,
                    tokenMoveSpeed * Time.deltaTime);
                yield return 0;
            }
            testTokenPlayer.transform.position = moveTarget;

    }*/
}
