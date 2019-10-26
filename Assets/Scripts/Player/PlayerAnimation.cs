using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int X = Animator.StringToHash("X");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
        }

        public void SetMovementDirection(Vector2 newDirection)
        {
            _animator.SetFloat(X, newDirection.x);
            _animator.SetFloat(Y, newDirection.y);
        }
    }
}
