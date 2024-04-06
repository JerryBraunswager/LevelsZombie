using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class DOTWorker : MonoBehaviour
{
    private TimeWork _timeWork;
    private Health _health;
    private SpriteRenderer _spriteRenderer;
    private List<DOTEffect> _effects = new List<DOTEffect>();
    private bool _isWhiteDelete = false;
    private bool _isActive = true;

    private void OnDisable()
    {
        _timeWork.TimeButtonPressed -= WorkWithTime;
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isActive)
        {
            for (int i = StaticConstants.Zero; i < _effects.Count; i++)
            {
                _effects[i].Apply(Time.deltaTime, _health);

                if (_effects[i].Duration <= StaticConstants.Zero)
                {
                    RemoveColor(_effects[i].Color);
                    _effects.RemoveAt(i);
                    i -= StaticConstants.One;
                }
            }

            if (_effects.Count == StaticConstants.Zero)
            {
                _spriteRenderer.color = Color.white;
                _isWhiteDelete = false;
            }
        }
    }

    public void AddEffect(DOTEffect applyEffect)
    {
        bool applyed = false;

        foreach (DOTEffect effect in _effects)
        {
            if (effect.NameOfEffect == applyEffect.NameOfEffect)
            {
                applyed = true;
                effect.IncreaseAllTime(applyEffect.Duration);
                break;
            }
        }

        if (!applyed)
        {
            _effects.Add(applyEffect);
            AddColor(applyEffect.Color);
        }
    }

    public void Init(TimeWork timeWork)
    {
        _timeWork = timeWork;
        _timeWork.TimeButtonPressed += WorkWithTime;
    }

    private void WorkWithTime(bool isTimeStop)
    {
        _isActive = !isTimeStop;
    }

    private void RemoveColor(Color color)
    {
        float r = _spriteRenderer.color.r - color.r;
        float g = _spriteRenderer.color.g - color.g;
        float b = _spriteRenderer.color.b - color.b;
        Color applyColor = new Color(r, g, b);
        _spriteRenderer.color = applyColor;
    }

    private void AddColor(Color color)
    {
        float r = _spriteRenderer.color.r + color.r;
        float g = _spriteRenderer.color.g + color.g;
        float b = _spriteRenderer.color.b + color.b;

        if(_isWhiteDelete == false)
        {
            r -= Color.white.r;
            g -= Color.white.g;
            b -= Color.white.b;
            _isWhiteDelete = true;
        }

        Color applyColor = new Color(r, g, b);;
        _spriteRenderer.color = applyColor;
    }
}
