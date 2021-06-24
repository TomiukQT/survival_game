using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private readonly float BULLET_LIFETIME = 5f;
    
    public void Setup(Vector3 dir, float damage, float speed = 10f)
    {
        _direction = dir;
        _damage = damage;
        _speed = speed;
        
        Destroy(gameObject,BULLET_LIFETIME);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }

}
