using Light;
using Player;
using UnityEngine;

namespace Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject _owner;
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _lifetime = 20f;
        private Rigidbody2D _rb2D;

        public void SetOwner(GameObject newOwner)
        {
            _owner = newOwner;
        }
        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            _lifetime -= Time.deltaTime;
            //error proofing projectiles that survive without hitting anything.
            if (_lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            var t = transform;
            var targetPosition = t.position += _speed * Time.fixedDeltaTime * t.right;
            _rb2D.MovePosition(targetPosition);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Colliding with player.
            if (other.CompareTag("Player"))
            {
                if (other.gameObject != _owner)
                {
                    other.GetComponent<PlayerHealth>().ChangeHealth(-1);
                    Destroy(gameObject);
                }
            }
            // Colliding with breakable object.
            else if (other.CompareTag("Breakable"))
            {
                other.GetComponent<Breakable>().Explode();
                Destroy(gameObject);
            }
            // Colliding with a lightable object.
            else if (other.CompareTag("Lightable"))
            {
                other.GetComponent<LightableObject>().LightUp();
                Destroy(gameObject);
            }
            // Colliding with anything else.
            else if (!(other.CompareTag("CollisionCheck") || other.CompareTag("SpawnBounds")))
            {
                Destroy(gameObject);
            }
        }
    }
}
