using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private int _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void ChangeHealth(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            print("Dead Witch");
        }
    }
}
