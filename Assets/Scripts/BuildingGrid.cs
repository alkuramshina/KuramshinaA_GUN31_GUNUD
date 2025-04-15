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
        PlaceBuilding();
    }

    private void PlaceBuilding()
    {
        if (_chosenBuilding is null) return;

        var placement = new Plane(Vector3.up, Vector3.zero);
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!placement.Raycast(ray, out var position)) return;
        
        var worldPosition = ray.GetPoint(position);
        
        var x = Mathf.RoundToInt(worldPosition.x);
        var y = Mathf.RoundToInt(worldPosition.z);

        var canBePlaced = (x >= 0 && x <= gridSize.x - _chosenBuilding.Size.x)
                          && (y >= 0 && y <= gridSize.y - _chosenBuilding.Size.y)
                          && !IsOccupied(x, y);

        _chosenBuilding.transform.position = new Vector3(x, 0, y);
        _chosenBuilding.RenderAvailability(canBePlaced);
        
        if (canBePlaced && Input.GetMouseButtonDown(0))
        {
            PlaceChosenBuilding(x, y);
        }
    }

    private bool IsOccupied(int placeX, int placeY)
    {
        for (var x = 0; x < _chosenBuilding.Size.x; x++)
        {
            for (var y = 0; y < _chosenBuilding.Size.y; y++)
            {
                if (_grid[placeX + x, placeY + y] is not null) 
                    return true;
            }
        }
        
        return false;
    }

    private void PlaceChosenBuilding(int placeX, int placeY)
    {
        for (var x = 0; x < _chosenBuilding.Size.x; x++)
        {
            for (var y = 0; y < _chosenBuilding.Size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _chosenBuilding;
            }
        }
        
        _chosenBuilding.RenderNormal();
        _chosenBuilding = null;
    }
}
