using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Balance : MonoBehaviour
{
    [SerializeField] private float _forceChange;
    [SerializeField] private float _targetRotation;
    [SerializeField] private float _forceBalance;
    [SerializeField] private float _maxForce = 1000;

    private float _startRotation;
    private Vector2 _force;
    private Rigidbody2D _rigidBody;
    private Vector2 _currentForce;
    private Actor _actor;
    private bool _isAlive = true;
    private bool _isStopped = false;

    private void Awake()
    {
        _actor = GetComponentInParent<Actor>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _actor.Died += Die;
        _actor.TimeWork.TimeButtonPressed += WorkWithTime;
    }


    private void OnDisable()
    {
        _actor.Died -= Die;
        _actor.TimeWork.TimeButtonPressed -= WorkWithTime;
    }

    private void Start()
    {
        _currentForce = _force;
        _startRotation = _targetRotation;
    }

    private void FixedUpdate()
    {
        if (_isAlive == true & _isStopped == false)
        {
            _rigidBody.MoveRotation(Mathf.LerpAngle(_rigidBody.rotation, _targetRotation, _forceBalance * Time.fixedDeltaTime));
            _rigidBody.AddForce(_currentForce);
            _currentForce = Vector2.MoveTowards(_currentForce, _force, _forceChange * Time.fixedDeltaTime);
        }
    }

    public void ChangeRotation(float rotation)
    {
        rotation = Mathf.Clamp(rotation, StaticConstants.Zero, StaticConstants.FullCircle - StaticConstants.One);
        _targetRotation = _startRotation + rotation;
    }

    public void AddForce(float force)
    {
        force = Mathf.Clamp(force, StaticConstants.Zero, _maxForce);
        _currentForce += Vector2.right * force;
    }

    private void WorkWithTime(bool isTimeStop)
    {
        _isStopped = isTimeStop;
    }

    private void Die()
    {
        _isAlive = false;
    }
}
