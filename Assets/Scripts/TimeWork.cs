using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeWork : MonoBehaviour
{
    [SerializeField] private KeyCode _key;
    private bool _isTimeStop = false;

    public event UnityAction<bool> TimeButtonPressed;

    private void Update()
    {
        if(Input.GetKeyDown(_key))
        {
            if(_isTimeStop) 
            {
                _isTimeStop = false;
            }
            else
            {
                _isTimeStop = true;
            }

            TimeButtonPressed.Invoke(_isTimeStop);
        }
    }
}
