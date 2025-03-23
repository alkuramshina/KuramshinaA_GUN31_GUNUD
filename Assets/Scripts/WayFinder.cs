using System.Collections;
using UnityEngine;

public class WayFinder : MonoBehaviour
{
    [SerializeField] private Rigidbody selfRigidbody;
    [SerializeField] private float seeingDistance;
    [SerializeField] private float speed;
    [SerializeField, Range(-1, 1)] private float rotationAngle;

    private bool _isRotating;
    
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
        
        if (_isRotating)
            return;

        if (IsAnyObstacleInDistance())
        {
            _isRotating = true;
            
            StartCoroutine(RandomRotation());
        }
        else
        {
            StartCoroutine(MovingForward());
        }
    }
    
    private IEnumerator MovingForward()
    {
        while (!IsAnyObstacleInDistance())
        {
            selfRigidbody.velocity = transform.forward * speed;
            yield return null;
        }
        
        yield return null;
    }
    
    private IEnumerator RandomRotation()
    {
        while (IsAnyObstacleInDistance())
        {
            transform.Rotate(new Vector3(0, rotationAngle, 0));
            yield return null;
        }
        
        _isRotating = false;
        yield return null;
    }

    private bool IsAnyObstacleInDistance()
    {
        return selfRigidbody.SweepTestAll(transform.forward, seeingDistance).Length > 0;
    }
}
