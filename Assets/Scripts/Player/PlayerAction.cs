﻿using System;
using Projectile;
using UnityEngine;

namespace Player
{
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private float _actionCooldown;
        [SerializeField] private KeyCode _actionKey = KeyCode.E;
        [SerializeField] private GameObject _flamePrefab;
        private float _timeSinceAction;
        private int _ammo = 1;
        
        public void ChangeAmmo(int amount)
        {
            _ammo += amount;
        }
        private void Awake()
        {
            _timeSinceAction = _actionCooldown;
        }

        private void Update()
        {
            // Update the cooldown.
            _timeSinceAction += Time.deltaTime;
            _timeSinceAction = Mathf.Clamp(_timeSinceAction, 0, _actionCooldown);
            // Spawn the flame.
            if (Input.GetKeyDown(_actionKey) && _ammo > 0 && _timeSinceAction >= _actionCooldown)
            {
                // _ammo--;
                _timeSinceAction = 0;
                SpawnFlame();
            }
        }

        private void SpawnFlame()
        {
            var spawnPos = transform.position / 1.6f;
            var spawnX = Mathf.Round(spawnPos.x) * 1.6f;
            var spawnY = Mathf.Round(spawnPos.y) * 1.6f;
            spawnPos= new Vector3(spawnX, spawnY, 0);
            Instantiate(_flamePrefab, spawnPos, Quaternion.identity);
        }
    }
}
