using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private Transform testBuilding;
    private GridXZ<GridObject> grid;

    private void Awake()
    {
        int _gridWidth = 10;
        int _gridHeight = 10;
        float _cellSize = 10f;

        grid = new GridXZ<GridObject>(_gridHeight, _gridWidth, _cellSize, 
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Utils.GetMouseWorldPosition();
            grid.GetXZ(pos, out int x, out int z);

            GridObject gridObject = grid.GetValue(x, z);
            if (gridObject.CanBuild())
            {
                Transform  buildingTransform = Instantiate(testBuilding, grid.GetWorldPosition(x, z), Quaternion.identity);
                gridObject.SetTransform(buildingTransform);
            }
            else
            {
                Debug.Log("You cannot build there!");
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Utils.GetMouseWorldPosition();
            grid.GetXZ(pos, out int x, out int z);

            GridObject gridObject = grid.GetValue(x, z);
            if (!gridObject.CanBuild())
            {
                Destroy(gridObject.ClearTransform().gameObject);
            }
        }
    }
}
