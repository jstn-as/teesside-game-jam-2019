using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private int _rockCount;
        [SerializeField] private GameObject _rockPrefab;
        [SerializeField] private int _pumpkinCount;
        [SerializeField] private GameObject _pumpkinPrefab;
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
            // Spawn the pumpkins.
            for (var i = 0; i < _pumpkinCount; i++)
            {
                var pumpkinPosition = Spawner.GetSpawnPosition(_collider2D);
                Instantiate(_pumpkinPrefab, pumpkinPosition, Quaternion.identity, transform);
            }
        }
    }
}
