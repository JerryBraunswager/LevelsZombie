using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Actor
{
    [SerializeField] private List<Health> _keyLimbs;

    protected override bool IsDied(string name, float value)
    {
        foreach(Health limbHealth in _keyLimbs) 
        {
            if(limbHealth.transform.name == name && value <= 0) 
            {
                return true;
            }
        }

        return false;
    }
}
