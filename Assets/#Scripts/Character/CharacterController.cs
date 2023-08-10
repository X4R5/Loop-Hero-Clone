using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;

    Animator _animator;
    PathTile _currentTile;
    bool _canMove = false;

    [SerializeField] float _moveSpeed = 1f;

    private void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_currentTile == null) return;

        transform.position = Vector2.MoveTowards(transform.position, _currentTile.GetNextTile().transform.position, _moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _currentTile.GetNextTile().transform.position) < 0.01f)
        {
            _currentTile = _currentTile.GetNextTile();
            SetMoveAnimationTrigger();
        }
    }

    private void SetMoveAnimationTrigger()
    {
        if (_currentTile.GetNextTile().transform.position.y > _currentTile.transform.position.y)
        {
            _animator.SetTrigger("moveUp");
        }
        else if (_currentTile.GetNextTile().transform.position.y < _currentTile.transform.position.y)
        {
            _animator.SetTrigger("moveDown");
        }
        else if (_currentTile.GetNextTile().transform.position.x > _currentTile.transform.position.x)
        {
            _animator.SetTrigger("moveRight");
        }
        else if (_currentTile.GetNextTile().transform.position.x < _currentTile.transform.position.x)
        {
            _animator.SetTrigger("moveLeft");
        }
    }

    public void StartMove(PathTile tile)
    {
        transform.position = tile.transform.position;
        _canMove = true;
        _currentTile = tile;
    }
}
