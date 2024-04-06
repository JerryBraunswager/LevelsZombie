using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DOTEnchantment : Enchantment
{
    [SerializeField] private float _allTime;
    [SerializeField] private string _nameOfEffect;
    [SerializeField] private Color _color;
    [SerializeField] private float _damageReduction;

    public override bool OnHit(Health health, float damage)
    {
        DOTEffect effect = new DOTEffect();
        effect.Init(TimeToWait, _allTime, damage / _damageReduction, _nameOfEffect, _color);

        if (health.transform.TryGetComponent(out DOTWorker worker))
        {
            worker.AddEffect(effect);
        }

        return true;
    }

    public override bool OnShoot(float lookAngle, Wand wand, int index)
    {
        StartCoroutine(WaitForShoot(lookAngle, index));
        return false;
    }
}
