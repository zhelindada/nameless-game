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
        //MoveByTransform();
    }
    private void FixedUpdate()
    {
        MoveByRigidBody();
    }

    private void MoveByRigidBody() {

        Vector2 movement = new Vector2(_speed * Input.GetAxisRaw("Horizontal"), _speed * Input.GetAxisRaw("Vertical"));
        rigidbody2d.velocity = movement;
    }

    private void MoveByTransform() {
        Vector3 deltaLoc = new Vector3(_speed * Input.GetAxisRaw("Horizontal"), _speed * Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime;
        Vector3 newLoc = _playerTransform.position + deltaLoc;
        _playerTransform.position = newLoc;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }

    public void UpdateDirection() {
        if (!CanMove)
            return;
        if (Input.GetKeyDown(KeyCode.W)) {
            facingDirection = Vector2.up; 
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            facingDirection = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            facingDirection = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            facingDirection = Vector2.right;
        }
    }

    
}
