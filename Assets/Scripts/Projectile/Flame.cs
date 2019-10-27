using Player;
using UnityEngine;

namespace Projectile
{
    public class Flame : MonoBehaviour
    {
        [SerializeField] private float _fuseTime = 2f;
        [SerializeField] private float _shakeTime = 0.5f;
        [SerializeField] private GameObject _projectilePrefab;
        private Animator _animator;
        private static readonly int Explode = Animator.StringToHash("explode");
        private TransformShake _transformShake;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _transformShake = FindObjectOfType<TransformShake>();
        }
        private void Update()
        {
            
            // Detonate the flame once the fuse runs out.
            if (_fuseTime > 0)
            {
                _fuseTime -= Time.deltaTime;
            }
            else
            {
                DetonateFlame();
                _fuseTime += 100;
            }
        }

        private void DetonateFlame()
        {
            // Spawn the flame projectiles.
            var currentRotation = transform.rotation;
            for (var i = 0; i < 4; i++)
            {
                Instantiate(_projectilePrefab, transform.position + currentRotation * Vector3.right, currentRotation);
                currentRotation *= Quaternion.Euler(Vector3.forward * 90);
            }
            _transformShake.SetShakeTime(_shakeTime);
            _animator.SetTrigger(Explode);
            // Destroy(gameObject, _destroyPause);
        }

        public void DestroyFlame()
        {
            Destroy(gameObject);
        }
    }
}
