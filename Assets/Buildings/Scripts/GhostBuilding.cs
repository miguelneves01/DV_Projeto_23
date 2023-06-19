using System;
using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    private Transform _visual;

    private void Start()
    {
        RefreshVisual();

        GridBuildingSystem.Instance.OnSelectedBuildingChange += GhostBuilding_OnSelectedBuildingChange;
    }

    private void GhostBuilding_OnSelectedBuildingChange(object sender, EventArgs e)
    {
        Debug.Log("Selected Building Updated");
        RefreshVisual();
    }

    private void LateUpdate()
    {
        var targetPos = GridBuildingSystem.Instance.GetMouseWorldPositionXZ();
        targetPos.y = 1f;
        LeanTween.move(transform.gameObject, targetPos, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation,
            GridBuildingSystem.Instance.GetPlacedBuildingRotation(), Time.deltaTime * 10f);
    }

    private void RefreshVisual()
    {
        ClearVisual();

        if (!GridBuildingSystem.Instance.BuildMode) return;

        var placedBuildingSo = GridBuildingSystem.Instance.SelectedPlacedBuildingSo;

        if (placedBuildingSo != null)
        {
            _visual = Instantiate(placedBuildingSo.Visual, transform);
            _visual.localPosition = Vector3.zero;
        }
    }

    private void ClearVisual()
    {
        if (_visual != null)
        {
            Destroy(_visual.gameObject);
            _visual = null;
        }
    }
}