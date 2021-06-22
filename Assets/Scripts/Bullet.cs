using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private Vector3 _direction;
    [SerializeField] private float _speed;

    public void Setup(Vector3 dir)
    {
        _direction = dir;
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
