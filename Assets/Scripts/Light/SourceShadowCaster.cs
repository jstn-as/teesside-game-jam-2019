using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Light
{
    public class SourceShadowCaster : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private int _rayCount;
        private int _wallLayer;
        private MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _wallLayer = LayerMask.GetMask("Wall");
        }

        private void Update()
        {
            // Get the points for the mesh.
            var vertices = new Vector3[_rayCount + 1];
            vertices[0] = transform.position;
            for (var i = 1; i <= _rayCount; i++)
            {
                var angle = (float)i / _rayCount * 360f * Mathf.Deg2Rad;
                var rayDirection = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
                var hit = Physics2D.Raycast(transform.position, rayDirection, _maxDistance, _wallLayer);
                Vector2 targetPoint;
                if (hit.collider)
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = transform.position + rayDirection * _maxDistance;
                }
                Debug.DrawLine(transform.position, targetPoint, Color.red);
                vertices[i] = targetPoint;
            }
            // Get the triangles.
            var triangles = new int[_rayCount * 3];
            for (var i = 1; i < _rayCount; i++)
            {
                triangles[3 * i] = 0; 
                triangles[3 * i + 1] = i;
                triangles[3 * i + 2] = i == _rayCount ? 1 : i + 1;
            }
            print(triangles);
            // Create the new mesh.
            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            _meshFilter.mesh = mesh;
        }
    }
}
