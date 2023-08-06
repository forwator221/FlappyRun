using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    private Transform _target;
    private void Start()
    {
        _target = _pointA;
    }

    private void Update()
    {
        Move();
    }         

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Respawn();
        }
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (transform.position == _target.position)
        {
            _target = (_target == _pointA) ? _pointB : _pointA;
        }
    }
}
