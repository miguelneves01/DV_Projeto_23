using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem Instance;

    [SerializeField] private readonly float _cellSize = 10f;
    [SerializeField] private readonly int _gridHeight = 10;
    [SerializeField] private readonly int _gridWidth = 10;

    private PlacedBuildingSO.Dir _curDir = PlacedBuildingSO.Dir.Down;


    private GridXZ<GridObject> _grid;

    [SerializeField] private Transform _parentTransform;
    public PlacedBuildingSO SelectedPlacedBuildingSo { private set; get; }
    public bool BuildMode { set; get; }

    public event EventHandler OnSelectedBuildingChange;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;

        BuildMode = false;

        _grid = new GridXZ<GridObject>(_gridHeight, _gridWidth, _cellSize,
            (grid, x, z) => new GridObject(grid, x, z));
        
    }

    private void Update()
    {
        if (!SceneController.IsSceneActive("3D"))
        {
            return;
        }

        if (!BuildMode)
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
        if (!Input.GetMouseButtonDown(0)) return;

        var pos = Utils.GetMouseWorldPosition();

        int x, z;
        try
        {
            _grid.GetXZ(pos, out x, out z);
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
            return;
        }

        if (!_grid.InGrid(x, z)) return;

        var gridObject = _grid.GetValue(x, z);

        Debug.Log(gridObject);

        if (gridObject.HasBuilding())
        {
            gridObject.Interact();
        }
    }

    private void HandleLeftClick()
    {
        if (!Input.GetMouseButtonDown(0) || SelectedPlacedBuildingSo == null) return;

        Build();
    }

    private void Build()
    {
        var pos = Utils.GetMouseWorldPosition();
        _grid.GetXZ(pos, out var x, out var z);

        if (!_grid.InGrid(x, z)) return;

        if (IsEmptyGridPositionList(x, z, out var gridPositionList))
        {
            var buildingWorldPos = GetBuildingWorldPos(x, z);

            var placedBuilding = PlacedBuilding.Create(buildingWorldPos, new Vector2Int(x, z), _curDir,
                SelectedPlacedBuildingSo, _parentTransform);

            foreach (var gridPosition in gridPositionList)
                _grid.GetValue(gridPosition.x, gridPosition.y).SetPlacedBuilding(placedBuilding);

            ClearPlacedBuildingSO();
        }
        else
        {
            Debug.Log("You cannot build there!");
        }
    }

    private void HandleRightClick()
    {
        if (!Input.GetMouseButtonDown(1)) return;

        Demolish();

        SetBuildMode(false);
    }

    private void Demolish()
    {
        var pos = Utils.GetMouseWorldPosition();
        _grid.GetXZ(pos, out var x, out var z);

        if (!_grid.InGrid(x, z)) return;

        var gridObject = _grid.GetValue(x, z);

        if (!gridObject.HasBuilding()) return;

        CurrencySystem.Instance.AddCurrency(Mathf.CeilToInt(gridObject.GetPlacedBuildingSO().Cost * 0.5f));

        Destroy(gridObject.ClearPlacedBuilding().transform.gameObject);
    }

    private bool IsEmptyGridPositionList(int x, int z, out List<Vector2Int> gridPositionList)
    {
        gridPositionList = SelectedPlacedBuildingSo.GetGridPositionList(new Vector2Int(x, z), _curDir);

        var canBuild = true;
        foreach (var gridPosition in gridPositionList)
        {
            if (!_grid.InGrid(gridPosition.x, gridPosition.y))
            {
                canBuild = false;
                break;
            }

            if (_grid.GetValue(gridPosition.x, gridPosition.y).HasBuilding()) canBuild = false;
        }

        return canBuild;
    }

    private void HandleRotate()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;

        _curDir = PlacedBuildingSO.GetNextDir(_curDir);
    }

    public void ClearPlacedBuildingSO()
    {
        SelectedPlacedBuildingSo = null;
        BuildMode = false;
        OnSelectedBuildingChange?.Invoke(Instance, EventArgs.Empty);
    }

    public Vector3 GetMouseWorldPositionXZ()
    {
        var pos = Utils.GetMouseWorldPosition();
        _grid.GetXZ(pos, out var x, out var z);

        if (SelectedPlacedBuildingSo == null || !_grid.InGrid(x, z)) return pos;


        var worldPos = GetBuildingWorldPos(x, z);

        return worldPos;
    }

    private Vector3 GetBuildingWorldPos(int x, int z)
    {
        var cellSize = _grid.GetCellSize();
        var worldPos = _grid.GetWorldPosition(x, z);
        var rotationOffset = SelectedPlacedBuildingSo.GetRotationOffset(_curDir);
        var offset = new Vector3(rotationOffset.x, 0, rotationOffset.y) * cellSize;

        return worldPos + offset;
    }

    public Quaternion GetPlacedBuildingRotation()
    {
        return SelectedPlacedBuildingSo != null
            ? Quaternion.Euler(0, SelectedPlacedBuildingSo.GetRotationAngle(_curDir), 0)
            : Quaternion.identity;
    }

    public void SetBuildMode(bool buildMode)
    {
        BuildMode = buildMode;

        if (BuildMode) OnSelectedBuildingChange?.Invoke(Instance, EventArgs.Empty);
    }

    public void SetSelectedBuilding(PlacedBuildingSO building)
    {
        SelectedPlacedBuildingSo = building;

        OnSelectedBuildingChange?.Invoke(Instance, EventArgs.Empty);
    }

    public void DemolishRandomBuilding()
    {
        var list = _grid.GetValues();

        list = list.FindAll(o => o.HasBuilding());

        if (list.Count <= 0)
        {
            SceneController.LoadScene("GameOver");
            return;
        }

        var building = list[Random.Range(0, list.Count)];

        Destroy(building.ClearPlacedBuilding().transform.gameObject);
    }
}