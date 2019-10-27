using Player;
using Spawners;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private string _displayFormat = "Health: {0}";
        [SerializeField] private PlayerSpawner _player;
        private PlayerHealth _playerHealth;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (_player.GetPlayer())
            {
                if (!_playerHealth)
                {
                    _playerHealth = _player.GetPlayer().GetComponent<PlayerHealth>();
                }
                var currentHealth = _playerHealth.GetCurrentHealth();
                _text.text = string.Format(_displayFormat, currentHealth);
            }
        }
    }
}
