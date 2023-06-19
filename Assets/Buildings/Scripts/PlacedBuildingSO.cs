using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placed Building SO", menuName = "ShopItems/PlacedBuildings")]
public class PlacedBuildingSO : ShopItemSO
{
    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            case Dir.Down: return Dir.Left;
            case Dir.Left: return Dir.Up;
            case Dir.Up: return Dir.Right;
            case Dir.Right: return Dir.Down;
        }

        return Dir.Down;
    }

    public enum Dir
    {
        Down,
        Up,
        Left,
        Right,
    }

    [field: SerializeField] public int Height { private set; get; }
    [field: SerializeField] public int Width { private set; get; }
    [field: SerializeField] public Transform Prefab { private set; get; }
    [field: SerializeField] public Transform Visual { private set; get; }

    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            case Dir.Down: return 0;
            case Dir.Left: return 90;
            case Dir.Up: return 180;
            case Dir.Right: return 270;
        }

        return 0;
    }

    public Vector2Int GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            case Dir.Down: return new Vector2Int(0,0);
            case Dir.Left: return new Vector2Int(0, Width);
            case Dir.Up: return new Vector2Int(Width, Height);
            case Dir.Right: return new Vector2Int(Height, 0);
        }

        return new Vector2Int(0,0);
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPosList = new List<Vector2Int>();
        switch (dir)
        {
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        gridPosList.Add(offset + new Vector2Int(x,y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < Height; x++)
                {
                    for (int y = 0; y < Width; y++)
                    {
                        gridPosList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
        }

        return gridPosList;
    }

}
