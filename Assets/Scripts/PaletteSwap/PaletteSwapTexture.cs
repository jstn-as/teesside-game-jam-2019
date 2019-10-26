using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace PaletteSwap
{
    [ExecuteInEditMode]
    public class PaletteSwapTexture : MonoBehaviour
    {
        [SerializeField] private Vector3 _lightPosition;
        [SerializeField] private float _radius;
        [SerializeField] private Vector2Int _screenDimensions;
        [SerializeField] private int _screenScale;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;
        private Texture2D _texture;
        private Color[] _colors;

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            var renderRect = new Rect(0, 0, src.width, src.height);
            _texture = new Texture2D(src.width, src.height, GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None);
            _texture.ReadPixels(renderRect, 0, 0);
            _colors = new Color[src.width * src.width];
            for (var i = 0; i < src.width * src.width; i++)
            {
                var x = (i % _screenDimensions.x);
                var y = (i / _screenDimensions.x);
                
                var screenPos = new Vector3(x, y, 0);
                var worldPos =_camera.ScreenToWorldPoint(screenPos);

                // var distance = 1 - Vector3.Distance(worldPos, _lightPosition) / _radius;
                var distance = 1 - Vector3.SqrMagnitude(worldPos - _lightPosition) / _radius;
                _distance = distance;
                var originalColour = _texture.GetPixel(x, y);
                var targetColour = Color.Lerp(Color.black, originalColour, distance);
                _colors[i] = targetColour;
            }
            _texture.SetPixels(_colors);
            _texture.Apply();
            Graphics.Blit(_texture, dest);
        }
    }
}
