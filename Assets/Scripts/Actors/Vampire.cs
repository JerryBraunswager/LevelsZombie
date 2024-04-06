using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Actor
{
    protected override bool IsDied(string name, float value)
    {
        return false;
    }
}
