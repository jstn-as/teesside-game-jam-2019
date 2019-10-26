using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Light
{
    public class LineGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _shadowPrefab;
        [SerializeField] private Tilemap _tileMap;
        [SerializeField] private List<LightSource> _lights;
        [SerializeField] private float _shadowLength = 1;
        private List<Vector2> _blocks = new List<Vector2>();
        private List<Mesh> _meshes = new List<Mesh>();
        private void Awake()
        {
            // Find each tile on the tile map.
            var bounds = _tileMap.cellBounds;
            for (var y = 0; y < bounds.size.y; y++)
            {
                for (var x = 0; x < bounds.size.x; x++)
                {
                    var xPosition = x + bounds.position.x;
                    var yPosition = y + bounds.position.y;
                    var tilePosition = new Vector3Int(xPosition, yPosition, 0);
                    var targetTile = _tileMap.GetTile(tilePosition);
                    // Check if the block exists.
                    if (targetTile)
                    {
                        var cellSize = _tileMap.cellSize;
                        var blockPosition = new Vector2(xPosition * cellSize.x + cellSize.x / 2, yPosition * cellSize.y + cellSize.y / 2);
                        _blocks.Add(blockPosition);
                        // Create a mew shadow object.
                        var shadowObject = Instantiate(_shadowPrefab, Vector3.back, Quaternion.identity, transform);
                        // Create a new mesh for the shadow object.
                        var shadowMesh = new Mesh {vertices = new Vector3[4], triangles = new[] {0, 1, 2, 0, 2, 3}};
                        shadowObject.GetComponent<MeshFilter>().mesh = shadowMesh;
                        _meshes.Add(shadowMesh);
                    }
                }
            }
        }

        private void Update()
        {
            // Update the shadows of each block.
            for (var i = 0; i < _blocks.Count; i++)
            {
                var block = _blocks[i];
                // Per light shadows.
                for (var j = 0; j < _lights.Count; j++)
                {
                    var lightPos = _lights[j].transform.position;
                    var distance = Vector3.Distance(lightPos, block);
                    if (distance < _lights[j].GetLightRadius())
                    {
                        var cellSize = _tileMap.cellSize;
                        // Work out the positions of the shadow object vertices.
                        var bottomLeft = new Vector3(block.x - cellSize.x / 2, block.y - cellSize.y / 2);
                        var topLeft = new Vector3(block.x - cellSize.x / 2, block.y + cellSize.y / 2);
                        var topRight = new Vector3(block.x + cellSize.x / 2, block.y + cellSize.y / 2);
                        var bottomRight = new Vector3(block.x + cellSize.x / 2, block.y - cellSize.y / 2);
                        // Adjust them.
                        // Bottom left.
                        bottomLeft.x = lightPos.x < bottomLeft.x
                            ? bottomLeft.x
                            : bottomLeft.x - _shadowLength;
                        bottomLeft.y = lightPos.y < bottomLeft.y
                            ? bottomLeft.y
                            : bottomLeft.y - _shadowLength;
                        // Bottom right.
                        bottomRight.x = lightPos.x > bottomRight.x
                            ? bottomRight.x
                            : bottomRight.x + _shadowLength;
                        bottomRight.y = lightPos.y < bottomRight.y
                            ? bottomRight.y
                            : bottomRight.y - _shadowLength;
                        // Top left.
                        topLeft.x = lightPos.x < topLeft.x
                                    ? topLeft.x
                                    : topLeft.x - _shadowLength;
                        topLeft.y = lightPos.y > topLeft.y
                                    ? topLeft.y
                                    : topLeft.y + _shadowLength;
                        // Top right.
                        topRight.x = lightPos.x > topRight.x
                                     ? topRight.x
                                     : topRight.x + _shadowLength;
                        topRight.y = lightPos.x > topRight.y
                                     ? topRight.y
                                     : topRight.y + _shadowLength;
                        // Set the vertices of the mesh.
                        Debug.DrawLine(lightPos, topRight, Color.red);
                        var vertices = new Vector3[]
                        {
                            bottomLeft,
                            topLeft,
                            topRight,
                            bottomRight,
                        };
                        _meshes[i].vertices = vertices;
                    }
                }
            }
        }
    }
}