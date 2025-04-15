using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int size = Vector2Int.one;
    public Vector2Int Size => size;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void OnDrawGizmos()
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                Gizmos.color = (x + y) % 2 == 0 
                    ? new Color(.88f, 1, .3f)
                    : new Color(1, .68f, .3f);
                
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }

    public void RenderAvailability(bool isAvailable)
    {
        _renderer.material.color = isAvailable ? Color.green : Color.red;
    }

    public void RenderNormal()
    {
        _renderer.material.color = Color.white;
    }
}
