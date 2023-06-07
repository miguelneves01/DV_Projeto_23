public class GridObject
{
    private GridXZ<GridObject> _grid;
    private int _x;
    private int _z;
    private PlacedBuilding _placedBuilding;

    public GridObject(GridXZ<GridObject> grid, int x, int z)
    {
        this._grid = grid;
        this._x = x;
        this._z = z;
    }

    public void SetPlacedBuilding(PlacedBuilding placedBuilding)
    {
        this._placedBuilding = placedBuilding;
    }

    public bool HasBuilding()
    {
        return _placedBuilding == null;
    }

    public PlacedBuilding ClearPlacedBuilding()
    {
        PlacedBuilding currPlacedBuilding = _placedBuilding;
        this._placedBuilding = null;
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
}
