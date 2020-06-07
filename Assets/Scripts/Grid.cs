using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using System.Runtime.CompilerServices;

public class Grid<T>
{
    private int width;
    private int height;
    private float cellSize;
    private T[,] gridArray;
    private Vector3 originPosition;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createTobject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new T[width, height];
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j] = createTobject(this, i, j);
            }
        }
                for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.green, 100);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.green, 100);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.green, 100);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.green, 100);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldposition, out int i, out int j)
    {
        i = Mathf.FloorToInt((worldposition - originPosition).x / cellSize);
        j = Mathf.FloorToInt((worldposition - originPosition).y / cellSize);
    }

    public void SetGridObject(int i, int j, T value)
    { 
        if(i >= 0 && i < width && j >= 0 && j < height)
        {
            gridArray[i, j] = value;
        }
    }

    public void SetGridObject(Vector3 worldPosition, T value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public T GetGridObject(int i, int j)
    {
        if (i >= 0 && i < width && j >= 0 && j < height)
        {
            return gridArray[i, j];
        }
        return default; //se puede poner default(T) pero no es necesario pues el compilador ya sabe que tipo de dato retornar(esta en la firma del metodo)
    }

    public T GetGridObject(Vector3 worldPosition) 
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    public int  Width
    { 
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public float CellSize
    {
        get
        {
            return cellSize;
        }
    }
}
