using System;
using System.Threading;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private KeyCode _up;
        [SerializeField] private KeyCode _down;
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _right;
        [Header("Misc")]
        [SerializeField, Range(0, 1)] private float _speed;
        private Vector3 _targetPosition;
        private bool _isMoving = false;
        private const float _moveAmount = 1.6f;

        private void Awake()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            // Process player movement.
            if (!_isMoving)
            {
                if (Input.GetKey(_up))
                {
                    _targetPosition += Vector3.up * _moveAmount;
                    _isMoving = true;
                }
                else if (Input.GetKey(_down))
                {
                    _targetPosition += Vector3.down * _moveAmount;
                    _isMoving = true;
                }
                else if (Input.GetKey(_left))
                {
                    _targetPosition += Vector3.left * _moveAmount;
                    _isMoving = true;
                }
                else if (Input.GetKey(_right))
                {
                    _targetPosition += Vector3.right * _moveAmount;
                    _isMoving = true;
                }
            }
            // Move the player.
            if (Vector3.Distance(transform.position, _targetPosition) > 0.1f)
            {
                var newPosition = Vector3.Lerp(transform.position, _targetPosition, _speed);
                transform.position = newPosition;
            }
            else
            {
                transform.position = _targetPosition;
                _isMoving = false;
            }
        }
    }
}
