using System;
using UnityEngine;

namespace Light
{
    public class ModifyMesh : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;

        private void Awake()
        {
            var newMesh = new Mesh
            {
                vertices = new[]
                {
                    new Vector3(0,0), new Vector3(0.2f, 1), new Vector3(2, 2), new Vector3(1, 0.2f),
                },
                triangles = new[] {0, 1, 2, 0, 2, 3}
            };
            _meshFilter.mesh = newMesh;
        }
    }
}