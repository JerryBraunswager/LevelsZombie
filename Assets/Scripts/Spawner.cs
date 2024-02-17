using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeToWait;
    [SerializeField] private Bullet _bullet;

    private WaitForSeconds _sleep;

    private void Start()
    {
        _sleep = new WaitForSeconds(_timeToWait);
        StartCoroutine(StartShoot());
    }

    private IEnumerator StartShoot() 
    { 
        while(true) 
        {
            Instantiate(_bullet, transform, false);
            yield return _sleep;
        }
    }
}
