using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _cellSize;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;

        gridArray = new int[width,height];

        //Debug.Log(width + " , " + height);
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {

            }
        }
    }

    private Vector3 GetWorldPos(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }

    private void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPos.x / _cellSize);
        y = Mathf.FloorToInt(worldPos.y / _cellSize);
    }
}
