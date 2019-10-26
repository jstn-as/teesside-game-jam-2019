using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Light
{
    public class LineGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _tileMap;
        [SerializeField] private Transform _player;
        [SerializeField] private List<List<bool>> _blocks;
        [SerializeField] private List<Wall> _walls;

        private void Awake()
        {
            // Find each tile on the tile map.
            _blocks = new List<List<bool>>();
            var bounds = _tileMap.cellBounds;
            for (var y = 0; y < bounds.size.y; y++)
            {
                var row = new List<bool>();
                for (var x = 0; x < bounds.size.x; x++)
                {
                    var xPosition = x + bounds.position.x;
                    var yPosition = y + bounds.position.y;
                    var tilePosition = new Vector3Int(xPosition, yPosition, 0);
                    var targetTile = _tileMap.GetTile(tilePosition);
                    row.Add(targetTile);
                }
                _blocks.Add(row);
            }
            // Find all the walls.
            for (var y = 0; y < _blocks.Count; y++)
            {
                for (var x = 0; x < _blocks[0].Count; x++)
                {
                    // Check if there's a block in the position.
                    if (_blocks[y][x])
                    {
                        
                    }
                }
            }
        }

        private Vector2[] GetWallPoints(Vector2Int wallPosition, Direction wallDirection, Direction checkDirection)
        {
            return new[] {Vector2.zero, Vector2.zero};
        }
    }
}