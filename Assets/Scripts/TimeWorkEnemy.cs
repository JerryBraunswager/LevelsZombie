using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Animator))]
public class TimeWorkEnemy : MonoBehaviour
{
    [SerializeField] private TimeWork _timeWork;

    private Animator _animator;
    private bool _isDead = false;
    private Actor _enemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Actor>();
    }

    private void OnEnable()
    {
        _timeWork.TimeButtonPressed += WorkWithTime;
        _enemy.Died += Died;
    }

    private void OnDisable()
    {
        _timeWork.TimeButtonPressed -= WorkWithTime;
        _enemy.Died -= Died;
    }

    private void Died()
    {
        _isDead = true;
        _animator.SetBool(nameof(_isDead), true);
    }

    private void WorkWithTime(bool isTimeStop)
    {
        if(isTimeStop) 
        {
            if (_isDead == false)
            {
                //_animator.StartPlayback();
            }
        }
        else
        { 
            //_animator.StopPlayback();
        }
    }

}
