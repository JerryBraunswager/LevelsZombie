using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _forceChange;
    [SerializeField] private float _targetRotation;
    [SerializeField] private float _forceBalance;

    private Vector2 _force;
    private Rigidbody2D _rigidBody;
    private Vector2 _currentForce;
    private Health _health;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _currentForce = _force;
    }

    private void FixedUpdate()
    {
        _rigidBody.MoveRotation(Mathf.LerpAngle(_rigidBody.rotation, _targetRotation, _forceBalance * Time.fixedDeltaTime));
        _rigidBody.AddForce(_currentForce);
        _currentForce = Vector2.MoveTowards(_currentForce, _force, _forceChange * Time.fixedDeltaTime);
    }

    public void AddForce(Vector2 force)
    {
        _currentForce += force;
    }

    public void DecreaseHealth(float health)
    {
        _health.DecreaseHealth(health);
    }
}
