using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Transform _playerTransform;
    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;

    public Vector2 facingDirection;
    public bool CanMove {
        get; set;
    }

    private void Awake()
    {
        if (_playerTransform == null)
            _playerTransform = transform;
        CanMove = true;
        facingDirection = Vector2.down;

        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent <Collider2D>();
    }

    private void Update() {
        //if(CanMove)
        //MoveByTransform();
    }
    private void FixedUpdate()
    {
        CanMove = !GetComponent<PlayerAbility>().IsDashing;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }

    public void UpdateDirection(Vector2 direction) {
        facingDirection = direction.normalized;
    }

    public void OnMove(float horizontal, float vertical)
    {
        if (!CanMove)
            return;
        Vector2 move = new Vector2(horizontal, vertical) * _speed;
        rigidbody2d.velocity = move;
    }


}
