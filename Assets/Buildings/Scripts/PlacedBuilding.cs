using UnityEngine;

public class PlacedBuilding : MonoBehaviour
{
    private PlacedBuildingSO.Dir _dir;
    private Vector2Int _origin;

    private PlacedBuildingSO _placedBuildingSO;

    public static PlacedBuilding Create(Vector3 worldPos, Vector2Int origin, PlacedBuildingSO.Dir dir,
        PlacedBuildingSO placedBuildingSO, Transform parent)
    {
        var placedObjectTransform = Instantiate(placedBuildingSO.Prefab, worldPos,
            Quaternion.Euler(0, placedBuildingSO.GetRotationAngle(dir), 0), parent);

        var placedBuilding = placedObjectTransform.GetComponent<PlacedBuilding>();

        placedBuilding._placedBuildingSO = placedBuildingSO;
        placedBuilding._origin = origin;
        placedBuilding._dir = dir;

        return placedBuilding;
    }

    public void Interact()
    {
        ShopManager.Instance.ShowShop(_placedBuildingSO.Name);
    }
}