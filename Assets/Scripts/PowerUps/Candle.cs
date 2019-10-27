using System;
using Light;
using Player;
using UnityEngine;

namespace PowerUps
{
    public class Candle : MonoBehaviour
    {
        [SerializeField] private GameObject _lightPrefab;

        public void LightUp()
        {
            foreach (var player in FindObjectsOfType<PlayerMovement>())
            {
                var playerTransform = player.transform;
                Instantiate(_lightPrefab, playerTransform.position, Quaternion.identity, playerTransform);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LightUp();
                Destroy(gameObject);
            }
        }
    }
}