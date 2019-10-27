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
            // Create the pitch dark layer.
            if (!_drawPitchBlack) return;
            var spriteRenderer = SpawnLayer(0);
            spriteRenderer.color = Color.black;
            // Create the layers of darkness.
            for (var i = 1; i < _layerCount - 1; i++)
            {
                SpawnLayer(i);
            }
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
