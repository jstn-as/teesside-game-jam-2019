using System;
using UnityEngine;

namespace Light
{
    public class DarknessLayers : MonoBehaviour
    {
        [SerializeField] private int _layerCount;
        [SerializeField] private GameObject _layerPrefab;
        [SerializeField] private bool _drawPitchBlack;

        private void Awake()
        {
            // Create the layers of darkness.
            for (var i = 0; i < _layerCount; i++)
            {
                SpawnLayer(i);
            }
            // Create the pitch dark layer.
            if (!_drawPitchBlack) return;
            var spriteRenderer = SpawnLayer(_layerCount);
            spriteRenderer.color = Color.black;
        }

        private SpriteRenderer SpawnLayer(int order)
        {
            var newLayer = Instantiate(_layerPrefab, transform);
            var spriteRender = newLayer.GetComponent<SpriteRenderer>();
            spriteRender.sortingOrder = 10 + order;
            return spriteRender;
        }
    }
}
