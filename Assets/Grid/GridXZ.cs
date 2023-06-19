using System;
using System.Collections.Generic;
using UnityEngine;

public class GridXZ<T>
{
    private readonly float cellSize;
    private readonly T[,] gridArray;
    private readonly int height;
    private readonly int width;

    public GridXZ(int height, int width, float cellSize, Func<GridXZ<T>, int, int, T> createGridObject)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;

        gridArray = new T[width, height];


        for (var x = 0; x < gridArray.GetLength(0); x++)
        for (var z = 0; z < gridArray.GetLength(1); z++)
            gridArray[x, z] = createGridObject(this, x, z);
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    public bool InGrid(int x, int z)
    {
        return x >= 0 && z >= 0 && x < width && z < height;
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public void SetValue(int x, int z, T value)
    {
        if (InGrid(x, z)) gridArray[x, z] = value;
    }

    public void SetValue(Vector3 worldPosition, T value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }

    public T GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
            return gridArray[x, z];
        return default;
    }

    public T GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public List<T> GetValues()
    {
        var list = new List<T>();
        for (var i = 0; i < width; i++)
        for (var j = 0; j < height; j++)
            list.Add(GetValue(i, j));

        return list;
    }
}