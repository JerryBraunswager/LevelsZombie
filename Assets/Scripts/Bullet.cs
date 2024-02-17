using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector2 _force;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidBody;

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent(out EnemyMove component)) 
        {
            component.AddForce(_force);
            component.DecreaseHealth(_force.x);
        }

        Destroy(gameObject);
    }
}
