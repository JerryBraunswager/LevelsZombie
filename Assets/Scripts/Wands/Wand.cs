using System.Collections;
using UnityEngine;

public class Wand : MonoBehaviour
{
    [Header("WandConfig")]
    [SerializeField] private Enchantment _wandEnchantment;
    [SerializeField] private float _damage;
    [SerializeField] private bool _isAllCast;
    [SerializeField] private int _castSlotsCount;
    [SerializeField] private float _timeToWaitForShoot;

    [Header("BulletConfig")]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Sprite _bulletSprite;

    [Header("Config")]
    [SerializeField] private Enchantment[] _enchantments;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Shoot _shoot;

    private int _currentEnchantment;
    private WaitForSeconds _sleep;

    private void Start()
    {
        ShootPoint = _shootPoint;
        _sleep = new WaitForSeconds(_timeToWaitForShoot);
    }

    private void OnEnable()
    {
        for (int i = 0;  i < _enchantments.Length; i++)
        {
            _enchantments[i].EnchantmentApplyed += NextEnchantment;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _enchantments.Length; i++)
        {
            _enchantments[i].EnchantmentApplyed -= NextEnchantment;
        }
    }

    public float Damage => _damage;

    public Sprite BulletSprite => _bulletSprite;

    public Bullet BulletPrefab => _bulletPrefab;

    public Transform ShootPoint { get; private set; }

    public void BulletHit(Health health, int index)
    {
        if (_isAllCast)
        {
            for (int i = StaticConstants.Zero; i < _enchantments.Length; i++)
            {
                _enchantments[i].OnHit(health, _damage);
            }
        }
        else
        {
            for (int i = index; i <= index + _castSlotsCount; i++)
            {
                _enchantments[i].OnHit(health, _damage);
            }
        }
    }

    public void Shoot(float lookAngle)
    {
        StartShooting(lookAngle);
    }

    private void StartShooting(float lookAngle)
    {
        _currentEnchantment = StaticConstants.Zero;
        _enchantments[_currentEnchantment].OnShoot(lookAngle, this, _currentEnchantment);
    }

    private void NextEnchantment(float lookAngle)
    {
        _currentEnchantment++;

        if (_currentEnchantment < _enchantments.Length)
        {
            _enchantments[_currentEnchantment].OnShoot(lookAngle, this, _currentEnchantment);
        }
        else if (_currentEnchantment == _enchantments.Length)
        {
            StartCoroutine(WaitForShoot());
        }
    }

    private IEnumerator WaitForShoot()
    {
        yield return _sleep;
        _shoot.CanShoot();
    }
}
