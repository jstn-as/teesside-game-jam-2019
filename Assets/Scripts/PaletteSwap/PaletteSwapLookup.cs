using UnityEngine;

namespace PaletteSwap
{
	[ExecuteInEditMode]
	public class PaletteSwapLookup : MonoBehaviour
	{
		[SerializeField] private Texture _lookupTexture;

		private Material _mat;
		private static readonly int PaletteTex = Shader.PropertyToID("_PaletteTex");

		private void OnEnable()
		{
			var shader = Shader.Find("Custom/Unlit/PaletteSwap");
			if (_mat == null)
				_mat = new Material(shader);
		}

		private void OnDisable()
		{
			if (_mat != null)
				DestroyImmediate(_mat);
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			_mat.SetTexture(PaletteTex, _lookupTexture);
			Graphics.Blit(src, dst,  _mat);
		}
	}
}
