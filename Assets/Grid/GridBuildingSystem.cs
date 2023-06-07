using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{

    [SerializeField] private PlacedBuildingSO _placedBuildingSO;
    private GridXZ<GridObject> _grid;
    private PlacedBuildingSO.Dir _curDir = PlacedBuildingSO.Dir.Down;

    private void Awake()
    {
        int _gridWidth = 10;
        int _gridHeight = 10;
        float _cellSize = 10f;

        _grid = new GridXZ<GridObject>(_gridHeight, _gridWidth, _cellSize, 
            (GridXZ<GridObject> grid, int x, int z) => new GridObject(grid, x, z));
    }

    public class GridObject
    {
        private GridXZ<GridObject> _grid;
        private int _x;
        private int _z;
        private Transform _transform;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this._grid = grid;
            this._x = x;
            this._z = z;
        }

        public void SetTransform(Transform transform)
        {
            this._transform = transform;
        }

        public bool CanBuild()
        {
            return _transform == null;
        }

        public Transform ClearTransform()
        {
            Transform curTransform = _transform;
            this._transform = null;
            return curTransform;
        }

        public override string ToString()
        {
            return _x + ", " + _z + "\n" + _transform;
        }
    }

    private void Update()
    {
        HandleLeftClick();

        HandleRightClick();

        HandleRotate();
    }

    private void HandleLeftClick()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }
        
        Vector3 pos = Utils.GetMouseWorldPosition();
        _grid.GetXZ(pos, out int x, out int z);

        if (!_grid.InGrid(x, z)) { return; }

        var gridPositionList = _placedBuildingSO.GetGridPositionList(new Vector2Int(x, z), _curDir);

        bool canBuild = true;
        foreach (var gridPosition in gridPositionList)
        {
            if (!_grid.InGrid(gridPosition.x, gridPosition.y))
            {
                canBuild = false;
                break;
            }

            if (!_grid.GetValue(gridPosition.x, gridPosition.y).CanBuild())
            {
                canBuild = false;
            }
        }


        GridObject gridObject = _grid.GetValue(x, z);

        if (canBuild)
        {
            var rotationOffset = _placedBuildingSO.GetRotationOffset(_curDir);
            Debug.Log(rotationOffset);
            Vector3 buildingWorldPos =
                _grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _grid.GetCellSize();
            Transform buildingTransform = Instantiate(_placedBuildingSO.Prefab, buildingWorldPos , Quaternion.Euler(0,_placedBuildingSO.GetRotationAngle(_curDir), 0));

            foreach (var gridPosition in gridPositionList)
            {
                _grid.GetValue(gridPosition.x, gridPosition.y).SetTransform(buildingTransform);
            }
        }
        else
        {
            Debug.Log("You cannot build there!");
        }
            
    }

    private void HandleRightClick()
    {
        if (!Input.GetMouseButtonDown(1)) { return; }
        
        Vector3 pos = Utils.GetMouseWorldPosition();
        _grid.GetXZ(pos, out int x, out int z);

        if (!_grid.InGrid(x, z)) { return; }

        GridObject gridObject = _grid.GetValue(x, z);

        if (gridObject.CanBuild()) { return; }
    
        Destroy(gridObject.ClearTransform().gameObject);   
        
    }

    private void HandleRotate()
    {
        if (!Input.GetKeyDown(KeyCode.R)) { return; }

        _curDir = PlacedBuildingSO.GetNextDir(_curDir);

        Debug.Log(_curDir);
    }
}
