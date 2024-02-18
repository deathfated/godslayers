using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Dictionary<Vector2, Tile> _tilesDic;
    private int _rows;
    private int _collumns;

    void Start()
    {
        _rows = transform.childCount;
        _collumns = transform.GetChild(0).childCount;

        _tilesDic = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _collumns ; x++)
        {
            for (int y = 0; y < _rows ; y++)
            {
                Tile _grabbedTile = transform.GetChild(y).GetChild(x).GetComponent<Tile>();

                _tilesDic[new Vector2(x,y)] = _grabbedTile;
            }
        }
    }

    public Tile GetTileAtPos(Vector2 pos)
    {
        if(_tilesDic.TryGetValue(pos,out Tile tile))
        {
            return tile;
        }
        
        return null;
    }
}
