using System;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public Vector2Int gridSize = new(10, 10);

    private Building[,] _grid;
    private Building _chosenBuilding;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _grid = new Building[gridSize.x, gridSize.y];
    }

    public void StartPlacing(Building buildingPrefab)
    {
        if (_chosenBuilding is not null)
        {
            Destroy(_chosenBuilding.gameObject);
        }
        
        _chosenBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        ChoosePlacement();
    }

    private void ChoosePlacement()
    {
        if (_chosenBuilding is null) return;

        var placement = new Plane(Vector3.up, Vector3.zero);
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!placement.Raycast(ray, out var position)) return;
        
        var worldPosition = ray.GetPoint(position);
        var x = Mathf.RoundToInt(worldPosition.x);
        var y = Mathf.RoundToInt(worldPosition.z);

        _chosenBuilding.transform.position = new Vector3(x, 0, y);

        if (Input.GetMouseButtonDown(0))
        {
            _chosenBuilding = null;
        }
    }
}
