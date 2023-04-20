using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridXZ<T>
{
    private int height;
    private int width;
    private float cellSize;
    private T[,] gridArray;
    private TextMesh[,] debugTextArray;
    private Transform debugParent;

    public GridXZ(int height, int width, float cellSize, System.Func<GridXZ<T>, int, int, T> createGridObject)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;

        debugParent = GameObject.Find("DebugGrid").transform;

        gridArray = new T[width, height];
        debugTextArray = new TextMesh[width, height];


        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = createGridObject(this, x, z);
            }
        }


        bool showDebug = true;
        if (showDebug)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < gridArray.GetLength(1); z++)
                {
                    debugTextArray[x, z] = Utils.CreateWorldText(gridArray[x, z]?.ToString(), Color.black, debugParent, GetWorldPosition(x ,z) + new Vector3(cellSize, 0 ,cellSize) * 0.5f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.black, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.black, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
        }
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {   
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public void SetValue(int x, int z, T value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height) 
        { 
            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
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
        {
            return gridArray[x, z];
        }
        else
        {
            return default;
        }
    }

    public T GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }

}
