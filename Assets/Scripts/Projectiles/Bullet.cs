using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private readonly int DefaultLayer = 0;
    private readonly int ZombieLayer = 6;

    [SerializeField] private float _force;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidBody;
    private Wand _wand;
    private SpriteRenderer _spriteRenderer;
    private int _index;

    public void Init(Wand wand, int index)
    {
        _index = index;

        if (_wand == null)
        {
            _wand = wand;
            _spriteRenderer.sprite = _wand.BulletSprite;
        }
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = transform.right * _speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == ZombieLayer || collision.gameObject.layer == DefaultLayer)
            {
                if (collision.transform.TryGetComponent(out Balance balance))
                {
                    balance.AddForce(_force);
                }

                if (collision.transform.TryGetComponent(out Health health))
                {
                    health.DecreaseHealth(_wand.Damage);
                    _wand.BulletHit(health, _index);
                }

                Destroy(gameObject);
            }
        }
    }
}
