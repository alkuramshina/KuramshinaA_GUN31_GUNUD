using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WayFinder : MonoBehaviour
{
    [SerializeField] private Rigidbody selfRigidbody;
    [SerializeField] private float seeingDistance;
    [SerializeField] private float speed;
    
    private readonly RaycastHit[] _result = new RaycastHit[1];

    private void RotateRandomly()
    {
        selfRigidbody.MoveRotation(Random.rotation);
    }

    private void FixedUpdate()
    {
        var collisions = selfRigidbody.SweepTestAll(transform.forward, seeingDistance);
        
        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
        
        
        if (collisions.Length == 0)
        {
            selfRigidbody.velocity = transform.forward * speed;
        }
        else
        {
            RotateRandomly();
            Debug.Log(Vector3.Distance(transform.position, collisions[0].collider.ClosestPoint(transform.position)));
        }
    }
}
