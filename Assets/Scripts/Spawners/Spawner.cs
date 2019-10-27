using UnityEngine;

namespace Spawners
{
    public static class Spawner
    {
        public static Vector2 GetSpawnPosition(Collider2D collider)
        {
            const float tileSize = 1.6f;
            var bounds = collider.bounds;
            var randomX = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x) / tileSize) * tileSize;
            var randomY = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y) / tileSize) * tileSize;
            var randomPosition = new Vector2(randomX, randomY);
            var boxSize = new Vector2(tileSize / 2, tileSize / 2);
            // Check if the position is empty.
            var results = Physics2D.OverlapBoxAll(randomPosition, boxSize, 0);
            if (results.Length > 0)
            {
                foreach (var result in results)
                {
                    if (!(result.CompareTag("CollisionCheck") || result.CompareTag("SpawnBounds")))
                    {
                        return GetSpawnPosition(collider);
                    }
                }
            }
            return randomPosition;
        }
    }
}