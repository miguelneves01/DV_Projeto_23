using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{

    [SerializeField] private PlacedBuildingSO _placedBuildingSO;
    private GridXZ<GridObject> _grid;
    private PlacedBuildingSO.Dir _curDir = PlacedBuildingSO.Dir.Down;

    private static bool _buildMode;

    public static bool BuildModeActive()
    {
        return _buildMode;
    }

    private void Awake()
    {
        _buildMode = false;

        int _gridWidth = 10;
        int _gridHeight = 10;
        float _cellSize = 10f;

        _grid = new GridXZ<GridObject>(_gridHeight, _gridWidth, _cellSize, 
            (GridXZ<GridObject> grid, int x, int z) => new GridObject(grid, x, z));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            _buildMode = !_buildMode;
            Debug.Log("BuildMode = " + _buildMode);
        }

        if (!_buildMode)
        {
            HandleInteract();

            return;
        }

        HandleLeftClick();

        HandleRightClick();

        HandleRotate();
    }

    private void HandleInteract()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }

        Vector3 pos = Utils.GetMouseWorldPosition();
        _grid.GetXZ(pos, out int x, out int z);

        if (!_grid.InGrid(x, z)) { return; }

        GridObject gridObject = _grid.GetValue(x, z);

        if (!gridObject.HasBuilding())
        {
            gridObject.Interact();
        }
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

            if (!_grid.GetValue(gridPosition.x, gridPosition.y).HasBuilding())
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

            PlacedBuilding placedBuilding = PlacedBuilding.Create(buildingWorldPos, new Vector2Int(x, z), _curDir, _placedBuildingSO);

            foreach (var gridPosition in gridPositionList)
            {
                _grid.GetValue(gridPosition.x, gridPosition.y).SetPlacedBuilding(placedBuilding);
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

        if (gridObject.HasBuilding()) { return; }
    
        Destroy(gridObject.ClearPlacedBuilding().transform.gameObject);   
        
    }

    private void HandleRotate()
    {
        if (!Input.GetKeyDown(KeyCode.R)) { return; }

        _curDir = PlacedBuildingSO.GetNextDir(_curDir);

        Debug.Log(_curDir);
    }
}
