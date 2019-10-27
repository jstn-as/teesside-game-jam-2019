using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _lightSourcePrefab;
        private GameObject _player;
        private Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            var randomPosition = Spawner.GetSpawnPosition(_collider2D);
            _player = Instantiate(_playerPrefab, randomPosition, Quaternion.identity, transform.parent);
            Instantiate(_lightSourcePrefab, randomPosition, Quaternion.identity);
        }

        public GameObject GetPlayer()
        {
            return _player;
        }
    }
}
