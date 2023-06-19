using UnityEngine;

public abstract class PlacedBuilding : MonoBehaviour
{
    private PlacedBuildingSO.Dir _dir;
    private Vector2Int _origin;

    public PlacedBuildingSO PlacedBuildingSO { private set; get; }

    public static PlacedBuilding Create(Vector3 worldPos, Vector2Int origin, PlacedBuildingSO.Dir dir,
        PlacedBuildingSO placedBuildingSO, Transform parent)
    {
        var placedObjectTransform = Instantiate(placedBuildingSO.Prefab, worldPos,
            Quaternion.Euler(0, placedBuildingSO.GetRotationAngle(dir), 0), parent);

        var placedBuilding = placedObjectTransform.GetComponent<PlacedBuilding>();

        placedBuilding.PlacedBuildingSO = placedBuildingSO;
        placedBuilding._origin = origin;
        placedBuilding._dir = dir;

        return placedBuilding;
    }

    public abstract void Interact();
}