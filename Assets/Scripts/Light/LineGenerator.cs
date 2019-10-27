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
        [SerializeField] private float _distanceOffset;
        [SerializeField] private float _shadowOffset = 0.4f;
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
                        var shadowMesh = new Mesh {vertices = new Vector3[4], triangles = new[] {0, 1, 2, 0, 1, 3, 0, 2, 3, 1, 2, 3}};
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
                    if (distance + _distanceOffset < _lights[j].GetLightRadius())
                    {
                        // Work out the positions of the shadow object vertices.
                        var top = block.y + _shadowOffset;
                        var bottom = block.y - _shadowOffset;
                        var right = block.x + _shadowOffset;
                        var left = block.x - _shadowOffset;
                        
                        var topLeft = new Vector3(left, top);
                        var topRight = new Vector3(right, top);
                        var bottomLeft = new Vector3(left, bottom);
                        var bottomRight = new Vector3(right, bottom);
                        
                        var newTopLeft = Vector3.zero;
                        var newTopRight = Vector3.zero;
                        var newBottomLeft = Vector3.zero;
                        var newBottomRight = Vector3.zero;

                        
                        
                        // newTopRight.y = lightPos.y < topRight.y ? topRight.y + (topRight.y - lightPos.y) : topRight.y;

//                        topLeft.x = lightPos.x > topLeft.x ? topLeft.x - _shadowLength : topLeft.x;
//                        topLeft.y = lightPos.y < topLeft.y ? topLeft.y + _shadowLength : topLeft.y;
//                        
//                        bottomRight.x = lightPos.x < bottomRight.x ? bottomRight.x + _shadowLength : bottomRight.x;
//                        bottomRight.y = lightPos.y > bottomRight.y ? bottomRight.y - _shadowLength : bottomRight.y;
//
//                        bottomLeft.x = lightPos.x > bottomLeft.x ? bottomLeft.x - _shadowLength : bottomLeft.x;
//                        bottomLeft.y = lightPos.y > bottomLeft.y ? bottomLeft.y - _shadowLength : bottomLeft.y;
                        
                        
//                        var nBl = Vector3.zero;
//                        var nBr = Vector3.zero;
//                        var nTl = Vector3.zero;
//                        var nTr = Vector3.zero;
                        // Adjust them.
//                        // Bottom left.
//                        nBl.x = Mathf.Clamp(oTl.x - (lightPos.x - oTl.x), -float.PositiveInfinity, oBl.x);
//                        nBl.y = Mathf.Clamp(oTl.y - (lightPos.y - oTl.y), -float.PositiveInfinity, oBl.y);
//
//                        nBr.x = Mathf.Clamp(oBr.x - (lightPos.x - oBr.x), oBl.x, float.PositiveInfinity);
//                        nBr.y = Mathf.Clamp(oBr.y - (lightPos.y - oBr.y), -float.PositiveInfinity, oTr.y);
//                        
//                        nTl.x = Mathf.Clamp(oTl.x - (lightPos.x - oTl.x), -float.PositiveInfinity, oTr.x);
//                        nTl.y = Mathf.Clamp(oTl.y - (lightPos.y - oTl.y), oBl.y, float.PositiveInfinity);
//
//                        nTr.x = Mathf.Clamp(oTr.x - (lightPos.x - oTr.x), oTl.x, float.PositiveInfinity);
//                        nTr.y = Mathf.Clamp(oTr.y - (lightPos.y - oTr.y), oBr.y, float.PositiveInfinity);
//                        if (lightPos.x <= oBr.x && lightPos.y >= oBr.y)
//                        {
//                            oBr.x -= (lightPos.x - oBr.x);
//                            oBr.y -= (lightPos.y - oBr.y);
//                        }
                        
//                        bottomLeft.x -= (lightPos.x - bottomLeft.x);
////                        bottomLeft.x = lightPos.x < bottomLeft.x
////                            ? bottomLeft.x
////                            : bottomLeft.x - (lightPos.x - bottomLeft.x);
//                        bottomLeft.y -= (lightPos.y - bottomLeft.y);
////                        bottomLeft.y = lightPos.y < bottomLeft.y
////                            ? bottomLeft.y
////                            : bottomLeft.y - (lightPos.y - bottomLeft.y);
////                        // Bottom right.
//                        bottomRight.x += (bottomRight.x - lightPos.x);
////                        bottomRight.x = lightPos.x > bottomRight.x
////                            ? bottomRight.x
////                            : bottomRight.x + (bottomRight.x - lightPos.x);
//                        bottomRight.y -= (lightPos.y - bottomRight.y);
//                        bottomRight.y = lightPos.y < bottomRight.y
//                            ? bottomRight.y
//                            : bottomRight.y - (lightPos.y - bottomRight.y);
//                        // Top left.
//                        topLeft.x -= (lightPos.x - topLeft.x);
////                        topLeft.x = lightPos.x < topLeft.x || lightPos.y > topLeft.y
////                                    ? topLeft.x
////                                    : topLeft.x - (lightPos.x - topLeft.x);
//                        topLeft.y += (topLeft.y - lightPos.y);
////                        topLeft.y = lightPos.y > topLeft.y || lightPos.x < topLeft.x
////                                    ? topLeft.y
////                                    : topLeft.y + (topLeft.y - lightPos.y);
//                        // Top right.
//                        topRight.x += (topRight.x - lightPos.x);;
////                        topRight.x = lightPos.x > topRight.x || lightPos.y > topRight.y
////                                     ? topRight.x
////                                     : topRight.x + (topRight.x - lightPos.x);
//                        topRight.y += (topRight.y - lightPos.y);
////                        topRight.y = lightPos.y > topRight.y || lightPos.x > topRight.x
////                                     ? topRight.y
////                                     : topRight.y + (topRight.y - lightPos.y);

                        // Set the vertices of the mesh.
//                        Debug.DrawLine(lightPos, topRight, Color.red);
                        var vertices = new[]
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