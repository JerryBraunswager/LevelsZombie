using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DOTEffect
{
    public string NameOfEffect { get; private set; }

    public float Damage { get; private set; }

    public float Duration { get; private set; }

    public float Delay { get; private set; }

    public Color Color { get; private set; }

    private float _currentDelay;

    public void Init(float delay, float allTime, float damage, string nameOfEffect, Color color)
    {
        Delay = delay;
        Duration = allTime;
        Damage = damage;
        NameOfEffect = nameOfEffect;
        _currentDelay = delay;
        Color = color;
    }

    public void IncreaseAllTime(float allTime)
    {
        Duration += allTime;
    }

    public void Apply(float deltaTime, Health health)
    {
        Duration -= deltaTime;
        _currentDelay -= deltaTime;

        if (_currentDelay < StaticConstants.Zero)
        {
            health.DecreaseHealth(Damage);
            _currentDelay = Delay;
        }
    }
}
