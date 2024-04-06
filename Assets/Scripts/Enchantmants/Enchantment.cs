using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enchantment : MonoBehaviour
{
    [SerializeField] private float _timeToWait;
    [SerializeField] private float _shotDelay;

    public event UnityAction<float> EnchantmentApplyed;

    private WaitForSeconds _sleep;
    private int _index;

    public float TimeToWait => _timeToWait;
    public int Index => _index;

    public abstract bool OnShoot(float lookAngle, Wand wand, int index);

    public abstract bool OnHit(Health health, float damage);

    protected IEnumerator WaitForShoot(float lookAngle, int index)
    {
        yield return _sleep;
        _index = index;
        EnchantmentApplyed.Invoke(lookAngle);
    }

    private void Start()
    {
        _sleep = new WaitForSeconds(_shotDelay);
    }
}
