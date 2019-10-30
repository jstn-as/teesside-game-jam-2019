using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _otherPlayer;
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private int _currentHealth;
        [SerializeField] private AudioClip _hurtClip;
        [SerializeField] private AudioClip _dieClip;

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }
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
                SfxPlayer.PlayAudio(_dieClip);
                Die();
            }
            else if (amount < 0)
            {
                SfxPlayer.PlayAudio(_hurtClip);
            }
        }

        public void Die()
        {
            print("Dead Witch");
            FindObjectOfType<WhoWon>().SetWinnerNumber(_otherPlayer);
            SceneManager.LoadScene(2);
        }
    }
}
