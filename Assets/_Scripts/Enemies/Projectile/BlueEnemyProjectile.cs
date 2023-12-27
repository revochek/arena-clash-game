using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueEnemyProjectile : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;

    [SerializeField] private NavMeshAgent _navMeshAgent;

    private GameObject _target;

    internal void Initialization(GameObject target)
    {
        _target = target;
    }

    private void Start()
    {
       _navMeshAgent = GetComponent<NavMeshAgent>();
       _navMeshAgent.speed = _speed;
       Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
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
