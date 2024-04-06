using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.IK;

public abstract class Actor : MonoBehaviour
{
    [SerializeField] private TimeWork _timeWork;
    [SerializeField] private float _maxHealthLimb;

    private List<Health> _limbs = new List<Health>();
    private List<Rigidbody2D> _bodies = new List<Rigidbody2D>();
    private float _health;
    private float _maxHealth;

    public TimeWork TimeWork => _timeWork;

    public event UnityAction<float, float> HealthChanged;

    public event UnityAction Died;

    protected abstract bool IsDied(string name, float value);

    private void Awake()
    {
        _limbs.Clear();
        GetLimbs();
    }

    private void Start()
    {
        enabled = true;
        HealthChanged.Invoke(_maxHealth, _maxHealth);
    }

    private void OnEnable()
    {
        foreach (Health limb in _limbs)
        {
            limb.HealthChanged += CalculateHealth;
            limb.Died += OnLimbDie;
        }

        _timeWork.TimeButtonPressed += WorkWithTime;
    }

    private void OnDisable()
    {
        foreach (Health limb in _limbs)
        {
            limb.HealthChanged -= CalculateHealth;
            limb.Died -= OnLimbDie;
        }

        _timeWork.TimeButtonPressed -= WorkWithTime;
    }

    private void WorkWithTime(bool isTimeStop)
    {
        for (int i = StaticConstants.Zero; i < _bodies.Count; i++)
        {
            if (isTimeStop)
            {
                _bodies[i].bodyType = RigidbodyType2D.Static;
            }
            else
            {
                _bodies[i].bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnLimbDie(Health limb)
    {
        if(limb.TryGetComponent(out Rigidbody2D body))
        {
            _bodies.Remove(body);
        }
    }

    private void CalculateHealth()
    {
        _health = 0;

        foreach (Health limbHealth in _limbs)
        {
            _health += limbHealth.Value;

            if (limbHealth.Value <= 0)
            {
                _limbs.Remove(limbHealth);
            }

            if (IsDied(limbHealth.transform.name, limbHealth.Value))
            {
                _health = 0;
                break;
            }
        }

        HealthChanged.Invoke(_health, _maxHealth);

        if (_health == 0)
        {
            _limbs.Clear();
            Died.Invoke();
        }
    }

    private void GetLimbs()
    {
        float childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Health health))
            {
                _limbs.Add(health);
                health.SetHealth(_maxHealthLimb);
                _health += health.MaxHealth;
                _maxHealth += health.MaxHealth;
            }

            if(transform.GetChild(i).TryGetComponent(out Rigidbody2D rigidbody))
            {
                _bodies.Add(rigidbody);
            }

            if(transform.GetChild(i).TryGetComponent(out DOTWorker worker)) 
            {
                worker.Init(_timeWork);
            }
        }
    }
}
