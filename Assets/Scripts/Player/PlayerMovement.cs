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
        [SerializeField] private float _speed;
        [SerializeField] private CollisionManager _collisionManager;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private bool _isMoving;
        private float _time;
        private const float MoveAmount = 1.6f;
        private const float DistanceThreshold = 0.01f;
        private PlayerAnimation _playerAnimation;
        private Rigidbody2D _rb2D;

        private void Awake()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb2D = GetComponent<Rigidbody2D>();
            // Set the target position to the spawn position.
            _targetPosition = transform.position;
            // Update the colliders.
            _rb2D.MovePosition(_targetPosition);
        }

        public bool IsMoving()
        {
            return _isMoving;
        }
        
        private void FixedUpdate()
        {
            // Process player movement.
            if (!_isMoving)
            {
                if (Input.GetKey(_upKey) && !_collisionManager.IsUp())
                {
                    _playerAnimation.SetMovementDirection(Vector2.up);
                    _targetPosition += Vector3.up * MoveAmount;
                    _startPosition = transform.position;
                    _isMoving = true;
                    _time = 0;
                }
                else if (Input.GetKey(_downKey) && !_collisionManager.IsDown())
                {
                    _playerAnimation.SetMovementDirection(Vector2.down);
                    _targetPosition += Vector3.down * MoveAmount;
                    _startPosition = transform.position;
                    _isMoving = true;
                    _time = 0;
                }
                else if (Input.GetKey(_leftKey) && !_collisionManager.IsLeft())
                {
                    _playerAnimation.SetMovementDirection(Vector2.left);
                    _targetPosition += Vector3.left * MoveAmount;
                    _startPosition = transform.position;
                    _isMoving = true;
                    _time = 0;
                }
                else if (Input.GetKey(_rightKey) && !_collisionManager.IsRight())
                {
                    _playerAnimation.SetMovementDirection(Vector2.right);
                    _targetPosition += Vector3.right * MoveAmount;
                    _startPosition = transform.position;
                    _isMoving = true;
                    _time = 0;
                }
            }
            // Move the player.
            if (Vector3.Distance(transform.position, _targetPosition) > DistanceThreshold)
            {
                // Using Lerp.
                // var newPosition = Vector3.Lerp(transform.position, _targetPosition, _speed);
                
                // Linear.
                var newPosition = Vector3.Lerp(_startPosition, _targetPosition, _time);
                _time += Time.fixedDeltaTime * _speed;
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
