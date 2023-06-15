using UnityEngine;

public class GridObject
{
    private GridXZ<GridObject> _grid;
    private PlacedBuilding _placedBuilding;
    private readonly int _x;
    private readonly int _z;

    public GridObject(GridXZ<GridObject> grid, int x, int z)
    {
        _grid = grid;
        _x = x;
        _z = z;
    }

    public void SetPlacedBuilding(PlacedBuilding placedBuilding)
    {
        _placedBuilding = placedBuilding;
    }

    public bool HasBuilding()
    {
        return _placedBuilding != null;
    }

    public PlacedBuilding ClearPlacedBuilding()
    {
        var currPlacedBuilding = _placedBuilding;
        _placedBuilding = null;
        return currPlacedBuilding;
    }

    public override string ToString()
    {
        return _x + ", " + _z + "\n" + _placedBuilding;
    }

    public void Interact()
    {
        _placedBuilding.Interact();
    }

    internal Vector3 GetPosition()
    {
        return new Vector3(_x, 0, _z);
    }
}