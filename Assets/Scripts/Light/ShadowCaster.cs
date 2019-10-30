using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Light
{
    public class ShadowCaster : MonoBehaviour
    {
        [SerializeField] private List<LightSource> _lights;
        [SerializeField] private float _shadowLength = 1;
        [SerializeField] private float _distanceOffset;
        [SerializeField] private float _halfTile = 0.8f;
        [SerializeField] private Tilemap _tileMap;
        [SerializeField] private GameObject _shadowPrefab;
        private readonly List<Vector2> _blocks = new List<Vector2>();
        private readonly List<Mesh> _meshes = new List<Mesh>();
        private readonly Vector3[] _emptyVertices = {Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};
        private readonly Vector3[][] _points = {
            // Stores the min and max points for each point.
            // top
            new[] {new Vector3(float.NegativeInfinity, float.PositiveInfinity), new Vector3(float.PositiveInfinity, float.PositiveInfinity)},
            // bottom.
            new[] {new Vector3(float.NegativeInfinity, float.NegativeInfinity), new Vector3(float.PositiveInfinity, float.NegativeInfinity)},
            // left.
            new[] {new Vector3(float.NegativeInfinity, float.NegativeInfinity), new Vector3(float.NegativeInfinity, float.PositiveInfinity)},
            // right.
            new[] {new Vector3(float.PositiveInfinity, float.NegativeInfinity), new Vector3(float.PositiveInfinity, float.PositiveInfinity)},
        };
        
        public void AddLight(LightSource lightSource)
        {
            _lights.Add(lightSource);
        }

        public void RemoveLight(LightSource lightSource)
        {
            _lights.Remove(lightSource);
        }
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
                        for (var i = 0; i < 4; i++)
                        {
                            // Create a mew shadow object.
                            var shadowObject = Instantiate(_shadowPrefab, transform);
                            shadowObject.name = $"{x} {y} {i}";
                            // Create a new mesh for the shadow object.
                            var shadowMesh = new Mesh {vertices = _emptyVertices, triangles = new[] {0, 1, 2, 0, 2, 3}};
                            shadowObject.GetComponent<MeshFilter>().mesh = shadowMesh;
                            _meshes.Add(shadowMesh);
                        }
                    }
                }
            }
        }
        private void Update()
        {
            // Update the shadows of each block.
            for (var i = 0; i < _blocks.Count; i++)
            {
                // Per light shadows.
                var block = _blocks[i];
                // Work out the positions of the shadow object vertices.
                var top = block.y + _halfTile;
                var bottom = block.y - _halfTile;
                var right = block.x + _halfTile;
                var left = block.x - _halfTile;
                        
                var topLeft = new Vector3(left, top);
                var topRight = new Vector3(right, top);
                var bottomLeft = new Vector3(left, bottom);
                var bottomRight = new Vector3(right, bottom);
                // Store the max and min positions for each quad.
                Vector3[][] points = {
                    // Stores the min and max points for each point.
                    // top
                    new[] {new Vector3(float.NegativeInfinity, float.PositiveInfinity), new Vector3(float.PositiveInfinity, float.PositiveInfinity)},
                    // bottom.
                    new[] {new Vector3(float.NegativeInfinity, float.NegativeInfinity), new Vector3(float.PositiveInfinity, float.NegativeInfinity)},
                    // left.
                    new[] {new Vector3(float.NegativeInfinity, float.NegativeInfinity), new Vector3(float.NegativeInfinity, float.PositiveInfinity)},
                    // right.
                    new[] {new Vector3(float.PositiveInfinity, float.NegativeInfinity), new Vector3(float.PositiveInfinity, float.PositiveInfinity)},
                };
                var index = i * 4;
                for (var j = 0; j < _lights.Count; j++)
                {
                    var lightPos = _lights[j].transform.position - new Vector3(.8f, .8f, 0);
                    var distance = Vector3.Distance(lightPos, block);
                    
                    // Modify the vertices of the shadow if the light is close enough.
                    if (distance < _lights[j].GetLightRadius() + _distanceOffset)
                    {
                        // Change the 4 meshes.
                        // Top.
                        {
                            var tl = topLeft + (bottomLeft - lightPos).normalized * _shadowLength;
                            var tr = topRight + (bottomRight - lightPos).normalized * _shadowLength;
                            tl.y = Mathf.Clamp(tl.y, bottom, float.PositiveInfinity);
                            tr.y = Mathf.Clamp(tr.y, bottom, float.PositiveInfinity);
                            if (lightPos.y > top)
                            {
                                points[0][0] = bottomLeft;
                                points[0][1] = bottomRight;
                            }
                            else
                            {
                                if (points[0][0] != bottomLeft && tl.x > points[0][0].x)
                                {
                                    points[0][0] = tl;
                                }

                                if (points[0][1] != bottomRight && tr.x < points[0][1].x)
                                {
                                    points[0][1] = tr;
                                }
                            }
                        }
                        // Bottom.
                        {
                            var bl = bottomLeft + (topLeft - lightPos).normalized * _shadowLength;
                            var br = bottomRight + (topRight - lightPos).normalized * _shadowLength;
                            bl.y = Mathf.Clamp(bl.y, float.NegativeInfinity, top);
                            br.y = Mathf.Clamp(br.y, float.NegativeInfinity, top);
                            if (lightPos.y < bottom)
                            {
                                points[1][0] = topLeft;
                                points[1][1] = topRight;
                            }
                            else
                            {
                                if (points[1][0] != topLeft && bl.x > points[1][0].x)
                                {
                                    points[1][0] = bl;
                                }

                                if (points[1][1] != topRight && br.x < points[1][1].x)
                                {
                                    points[1][1] = br;
                                }
                            }
                        }
                        // Left.
                        {
                            var bl = bottomLeft + (bottomRight - lightPos).normalized * _shadowLength;
                            var tl = topLeft + (topRight - lightPos).normalized * _shadowLength;
                            bl.x = Mathf.Clamp(bl.x, float.NegativeInfinity, right);
                            tl.x = Mathf.Clamp(tl.x, float.NegativeInfinity, right);
                            if (lightPos.x < left)
                            {
                                points[2][0] = bottomRight;
                                points[2][1] = topRight;
                            }
                            else
                            {
                                if (points[2][0] != bottomRight && bl.y > points[2][0].y)
                                {
                                    points[2][0] = bl;
                                }
                                if (points[2][1] != topRight && tl.y < points[2][1].y)
                                {
                                    points[2][1] = tl;
                                }
                            }
                        }
                        // Right.
                        {
                            var tr = topRight + (topLeft - lightPos).normalized * _shadowLength;
                            var br = bottomRight + (bottomLeft - lightPos).normalized * _shadowLength;
                            tr.x = Mathf.Clamp(tr.x, left, float.PositiveInfinity);
                            br.x = Mathf.Clamp(br.x, left, float.PositiveInfinity);
                            if (lightPos.x > right)
                            {
                                points[3][0] = topLeft;
                                points[3][1] = bottomLeft;
                            }
                            else
                            {
                                if (points[3][0] != bottomLeft && br.y > points[3][0].y)
                                {
                                    points[3][0] = br;
                                }
                                if (points[3][1] != topLeft && tr.y < points[3][1].y)
                                {
                                    points[3][1] = tr;
                                }
                            }
                        }
                    }
                }
                // Top.
                var isTopIntersecting = Intersection.IsIntersecting(bottomLeft, points[0][0], points[0][1], bottomRight,
                    out var topIntersection);
                if (isTopIntersecting)
                {
                    _meshes[index].vertices = new[] {bottomLeft, topIntersection, topIntersection, bottomRight};
                }
                else
                {
                    _meshes[index].vertices = new[] {bottomLeft, points[0][0], points[0][1], bottomRight};
                }
                // Bottom.
                var isBottomIntersecting = Intersection.IsIntersecting(points[1][0], topLeft, topRight, points[1][1],
                    out var bottomIntersection);
                if (isBottomIntersecting)
                {
                    _meshes[index + 1].vertices = new[] {bottomIntersection, topLeft, topRight, bottomIntersection};
                }
                else
                {
                    _meshes[index + 1].vertices = new[] {points[1][0], topLeft, topRight, points[1][1]};
                }
                // Left.
                var isLeftIntersecting = Intersection.IsIntersecting(bottomRight, points[2][0], topRight, points[2][1],
                    out var leftIntersection);
                if (isLeftIntersecting)
                {
                    _meshes[index + 2].vertices = new[] {leftIntersection, leftIntersection, topRight, bottomRight};
                }
                else
                {
                    _meshes[index + 2].vertices = new[] {points[2][0], points[2][1], topRight, bottomRight};
                }
                // Right.
                var isRightIntersecting = Intersection.IsIntersecting(bottomLeft, points[3][0], topLeft, points[3][1],
                    out var rightIntersection);
                if (isRightIntersecting)
                {
                    _meshes[index + 3].vertices = new[] {bottomLeft, topLeft, rightIntersection, rightIntersection};
                }
                else
                {
                    _meshes[index + 3].vertices = new[] {bottomLeft, topLeft, points[3][1], points[3][0]};
                }
            }
        }
        
        
    }
}