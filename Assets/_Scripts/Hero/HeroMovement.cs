using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeroMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public void Move(Vector2 direction, float speed)
    {
        if (direction.sqrMagnitude < 0.1)
            return;

        Vector3 targetVelocity = new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
        targetVelocity.y = _rb.velocity.y;
        _rb.velocity = targetVelocity;
    }
    public void ResetAngularVelocity()
    {
       _rb.velocity = Vector3.zero;
       _rb.angularVelocity = Vector3.zero;
    }
}
