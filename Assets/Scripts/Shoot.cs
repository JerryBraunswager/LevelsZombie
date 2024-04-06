using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Shoot : MonoBehaviour
{
    [SerializeField] private List<Balance> _limbs;
    [SerializeField] private Wand _wand;
    [SerializeField] private TimeWork _timeWork;

    private bool _isCanShoot = true;
    private bool _isTimeStop = false;
    private Vector3 lookDirection;
    private float lookAngle;

    private void OnEnable()
    {
        _timeWork.TimeButtonPressed += WorkWithTime;
    }

    private void OnDisable()
    {
        _timeWork.TimeButtonPressed -= WorkWithTime;
    }

    public void WorkWithTime(bool isTimeStop)
    {
        _isTimeStop = isTimeStop;
    }

    public void CanShoot()
    {
        _isCanShoot = true;
    }

    private void Update()
    {
        foreach (var limb in _limbs)
        {
            limb.ChangeRotation(lookAngle);
        }

        if (_isCanShoot && !_isTimeStop)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookAngle = SetLookAngle();

            if (Input.GetMouseButtonDown(StaticConstants.Zero))
            {
                _wand.Shoot(lookAngle);
                _isCanShoot = false;
            }
        }
    }

    private float SetLookAngle()
    {
        float y = lookDirection.y - _wand.ShootPoint.position.y;
        float x = lookDirection.x - _wand.ShootPoint.position.x;
        float angle = y / x * Mathf.Rad2Deg;
        angle = (angle < StaticConstants.Zero) ? StaticConstants.FullCircle + angle : angle;
        angle = (x < StaticConstants.Zero) ? StaticConstants.Zero : angle;
        return angle;
    }
}
