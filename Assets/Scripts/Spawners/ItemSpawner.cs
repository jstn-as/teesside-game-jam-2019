using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private float _rockCount;
        [SerializeField] private List<GameObject> _rocks;
        [SerializeField] private GameObject _rockPrefab;
        private Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            // Spawn the rocks.
            for (var i = 0; i < _rockCount; i++)
            {
                var rockPosition = Spawner.GetSpawnPosition(_collider2D);
                Instantiate(_rockPrefab, rockPosition, Quaternion.identity, transform);
            }
        }
    }
}
