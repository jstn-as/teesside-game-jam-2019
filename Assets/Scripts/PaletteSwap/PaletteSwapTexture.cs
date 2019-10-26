using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace PaletteSwap
{
    [ExecuteInEditMode]
    public class PaletteSwapTexture : MonoBehaviour
    {
        [SerializeField] private Vector2 _lightPosition;
        [SerializeField] private Vector2Int _screenDimensions;
        [SerializeField] private int _screenScale;
        [SerializeField] private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            var renderRect = new Rect(0, 0, src.width, src.height);
            var texture = new Texture2D(src.width, src.height, GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None);
            texture.ReadPixels(renderRect, 0, 0);
            var colourArray = new Color[src.width * src.width];
            for (var i = 0; i < colourArray.Length; i++)
            {
                var x = (i % _screenDimensions.x) / (float)_screenDimensions.x;
                var y = ((float)i / _screenDimensions.x) / _screenDimensions.y;
                
                var screenPos = new Vector3(x, y, 0);
                var worldPos =_camera.ScreenToWorldPoint(screenPos);

                var distance = Vector3.Distance(worldPos, _lightPosition);
                
                colourArray[i] = new Color(distance, distance, distance);
            }
            texture.SetPixels(colourArray);
            texture.Apply();
            Graphics.Blit(texture, dest);
        }
    }
}
