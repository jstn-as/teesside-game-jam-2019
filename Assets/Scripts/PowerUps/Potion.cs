using Player;
using UnityEngine;

namespace PowerUps
{
    public class Potion : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerAction>().ChangeAmmo(1);
                Destroy(gameObject);
            }
        }
    }
}
