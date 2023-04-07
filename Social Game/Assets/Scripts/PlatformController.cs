using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public Transform _platform;
    public Transform _startpoint;
    public Transform _endpoint;

    private int direction = 1;
    private float speed = 1f;

    void Start()
    {
        _platform.position = _startpoint.position;
    }


    private void Update() {
        Vector2 target = platformTarget();
        _platform.position = Vector2.MoveTowards(_platform.position, target, Time.deltaTime * speed);
         
        float distance = Vector2.Distance((Vector2) _platform.position, target);
        if(distance <= .1f)
        {
            direction *= -1;
        }
    }

    private Vector2 platformTarget()
    {
        if (direction == 1)
            return _startpoint.position;
        else
            return _endpoint.position;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(_startpoint.position, _endpoint.position);
    }
}
