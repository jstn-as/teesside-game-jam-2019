﻿using System;
using Player;
using UnityEngine;

namespace PowerUps
{
    public class Potion : MonoBehaviour
    {
        [SerializeField] private AudioClip _powerUpClip;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SfxPlayer.PlayAudio(_powerUpClip);
                other.GetComponent<PlayerAction>().ChangeAmmo(1);
                Destroy(gameObject);
            }
        }
    }
}
