using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody2D _rigidbody2D;
    private ContactFilter2D _contactFilter;
    private Vector2 _moveDirection;
    private List<RaycastHit2D> _hitBuffer = new List<RaycastHit2D>();

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    void Update()
    {
        _moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
    }

    private void FixedUpdate()
    {
        Vector2 move = _moveDirection * _speed * Time.deltaTime;
        Movement(move);
    }

    private void Movement(Vector2 move)
    {
        float distance = move.magnitude;

        int count = _rigidbody2D.Cast(move, _contactFilter, _hitBuffer, distance);

        for(int i = 0; i < count; i++)
        {
            Vector2 currentNormal = _hitBuffer[i].normal;
            float angle = Vector2.Dot(currentNormal, move);

            if(angle < 0)
            {
                move = move - currentNormal * angle;
            }
        }

        _rigidbody2D.position = _rigidbody2D.position + move.normalized * distance;
    }
}
