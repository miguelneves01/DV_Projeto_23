using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlacedBuilding : MonoBehaviour
{
    public static PlacedBuilding Create(Vector3 worldPos, Vector2Int origin, PlacedBuildingSO.Dir dir,
        PlacedBuildingSO placedBuildingSO)
    {
        Transform placedObjectTransform = Instantiate(placedBuildingSO.Prefab, worldPos, Quaternion.Euler(0, placedBuildingSO.GetRotationAngle(dir), 0));

        PlacedBuilding placedBuilding = placedObjectTransform.GetComponent<PlacedBuilding>();

        placedBuilding._placedBuildingSO = placedBuildingSO;
        placedBuilding._origin = origin;
        placedBuilding._dir = dir;

        return placedBuilding;
    }

    private PlacedBuildingSO _placedBuildingSO;
    private Vector2Int _origin;
    private PlacedBuildingSO.Dir _dir;

    public abstract void Interact();
}
