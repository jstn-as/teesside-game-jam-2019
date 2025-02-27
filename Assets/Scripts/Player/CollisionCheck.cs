﻿using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CollisionCheck : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _collidedObjects;

        public bool IsColliding()
        {
            // Remove missing objects.
            _collidedObjects.RemoveAll(item => item == null);
            return _collidedObjects.Count > 0;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherObject = other.gameObject;
            if (!_collidedObjects.Contains(otherObject) &&
                !(other.CompareTag("CollisionCheck") || other.CompareTag("SpawnBounds") || other.CompareTag("Player") ||
                  other.CompareTag("PowerUp")))
            {
                _collidedObjects.Add(otherObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _collidedObjects.Remove(other.gameObject);
        }
    }
}
