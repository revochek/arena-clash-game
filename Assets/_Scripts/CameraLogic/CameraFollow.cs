using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    [SerializeField]
    private float _smoothSpeed = 0.125f;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 targetPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed);
        smoothedPosition.y = transform.position.y;
        transform.position = smoothedPosition;
    }

    public void Follow(Transform target)
    {
        _target = target;
    }
}