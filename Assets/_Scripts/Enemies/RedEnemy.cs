using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] private float _freezeDuration;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _upMovementSpeed;
    [SerializeField] private float _swiftMovementForce;

    [SerializeField] private int _damage;

    private bool isFrozen = false;


    private void Update()
    {
        if (!isFrozen)
        {
            float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);

            if (distanceToTarget < AggroDistance)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (Target.transform.position - transform.position).normalized, out hit, AggroDistance))
                {
                    if (hit.collider.gameObject == Target)
                    {                        
                        if (transform.position.y < 0.5f)
                        {
                            _rigidbody.isKinematic = true;
                            transform.Translate(Vector3.up * _upMovementSpeed * Time.deltaTime);                           
                        }
                        else
                        {
                            StartCoroutine(FreezeAndMove());
                        }
                    }
                }
            }
        }
    }

    private IEnumerator FreezeAndMove()
    {
        isFrozen = true;
        yield return new WaitForSeconds(_freezeDuration);

        _rigidbody.isKinematic = false;

        Vector3 direction = (Target.transform.position - transform.position).normalized;
        _rigidbody.AddForce(direction * _upMovementSpeed * 5, ForceMode.Impulse);

        yield return new WaitForSeconds(_attackDuration);
        isFrozen = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out HeroHealth health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}