using System;
using System.Threading;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private KeyCode _upKey;
        [SerializeField] private KeyCode _downKey;
        [SerializeField] private KeyCode _leftKey;
        [SerializeField] private KeyCode _rightKey;
        [Header("Misc")]
        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private CollisionManager _collisionManager;
        private Vector3 _targetPosition;
        private bool _isMoving;
        private const float MoveAmount = 1.6f;
        private const float DistanceThreshold = 0.01f;
        private Rigidbody2D _rb2D;

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            // Set the target position to the spawn position.
            _targetPosition = transform.position;
            // Update the colliders.
            _rb2D.MovePosition(_targetPosition);
        }

        private void Update()
        {
            // Process player movement.
            if (!_isMoving)
            {
                if (Input.GetKey(_upKey) && !_collisionManager.IsUp())
                {
                    _targetPosition += Vector3.up * MoveAmount;
                    _isMoving = true;
                }
                else if (Input.GetKey(_downKey) && !_collisionManager.IsDown())
                {
                    _targetPosition += Vector3.down * MoveAmount;
                    _isMoving = true;
                }
                else if (Input.GetKey(_leftKey) && !_collisionManager.IsLeft())
                {
                    _targetPosition += Vector3.left * MoveAmount;
                    _isMoving = true;
                }
                else if (Input.GetKey(_rightKey) && !_collisionManager.IsRight())
                {
                    _targetPosition += Vector3.right * MoveAmount;
                    _isMoving = true;
                }
            }
            // Move the player.
            if (Vector3.Distance(transform.position, _targetPosition) > DistanceThreshold)
            {
                var newPosition = Vector3.Lerp(transform.position, _targetPosition, _speed);
                _rb2D.MovePosition(newPosition);
            }
            else
            {
                _rb2D.MovePosition(_targetPosition);
                _isMoving = false;
            }
        }
    }
}
