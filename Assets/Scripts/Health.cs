using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxPossibleHealth = 1000;

    private readonly float _minHealth = 0;
    private float _maxHealth;

    public float Value { get; private set; }

    public float MaxHealth => _maxHealth;

    public event UnityAction HealthChanged;
    public event UnityAction<Health> Died;

    public void DecreaseHealth(float health)
    {
        health = Mathf.Clamp(health, _minHealth, _maxHealth);
        Value -= health;
        HealthChanged.Invoke();

        if (Value <= _minHealth)
        {
            Died.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void SetHealth(float health)
    {
        _maxHealth = health;
        _maxHealth = Mathf.Clamp(_maxHealth, _minHealth, _maxPossibleHealth);
    }

    private void Start()
    {
        Value = _maxHealth;
    }
}