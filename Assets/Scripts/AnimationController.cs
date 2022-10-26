using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class AnimationController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _previousPosition;
    private int _speed = Animator.StringToHash("Speed");

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _previousPosition = _rigidbody2D.position;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = _rigidbody2D.position;

        if(_previousPosition == currentPosition)
        {
            _animator.SetFloat(_speed, 0);
        }
        else
        {
            _animator.SetFloat(_speed, 5);
        }

        _previousPosition = currentPosition;
    }
}
