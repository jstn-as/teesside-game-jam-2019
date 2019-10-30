using System;
using Projectile;
using UnityEngine;

namespace Player
{
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private float _actionCooldown;
        [SerializeField] private KeyCode _actionKey = KeyCode.E;
        [SerializeField] private GameObject _flamePrefab;
        [SerializeField] private AudioClip _castClip;
        private SfxPlayer _sfxPlayer;
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

        private void Start()
        {
            _sfxPlayer = FindObjectOfType<SfxPlayer>();
        }

        private void Update()
        {
            // Update the cooldown.
            _timeSinceAction += Time.deltaTime;
            _timeSinceAction = Mathf.Clamp(_timeSinceAction, 0, _actionCooldown);
            // Spawn the flame.
            if (Input.GetKeyDown(_actionKey) && _ammo > 0 && _timeSinceAction >= _actionCooldown)
            {
                SpawnFlame();
            }
        }

        private void SpawnFlame()
        {
            var position = transform.position;
            var spawnPos = position / 1.6f;
            var spawnX = Mathf.Round(spawnPos.x) * 1.6f;
            var spawnY = Mathf.Round(spawnPos.y) * 1.6f;
            spawnPos = new Vector3(spawnX, spawnY, 0);
            // Check if the position is clear.
            var results = Physics2D.OverlapBoxAll(spawnPos, Vector2.one * 0.4f, 0);
            foreach (var coll in results)
            {
                if (coll.CompareTag("Flame"))
                {
                    return;
                }
            }
            // Spawn the flame.
            SfxPlayer.PlayAudio(_castClip);
            _ammo--;
            _timeSinceAction = 0;
            var flame = Instantiate(_flamePrefab, spawnPos, Quaternion.identity);
            flame.GetComponent<Flame>().SetOwner(gameObject);
        }
    }
}
