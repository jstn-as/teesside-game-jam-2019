using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Light
{
    public class LightSource : MonoBehaviour
    {
        [SerializeField] private int _layerCount;
        [SerializeField] private Vector2 _baseScaleBounds;
        [SerializeField] private AnimationCurve _scaleChangeCurve;
        [SerializeField] private Sprite _maskSprite;
        [SerializeField] private List<SpriteMask> _spriteMasks;

        public float GetLightRadius()
        {
            var dropOff = _scaleChangeCurve.Evaluate(_layerCount);
            return _baseScaleBounds.y + dropOff;
        } 
        private void Awake()
        {
            // Create the sprite masks.
            for (var i = 0; i < _layerCount + 1; i++)
            {
                var spriteObject = new GameObject($"Sprite Mask {i}", typeof(SpriteMask));
                spriteObject.transform.SetParent(transform);
                spriteObject.transform.localPosition = Vector3.zero;

                var spriteMask = spriteObject.GetComponent<SpriteMask>();
                _spriteMasks.Add(spriteMask);
                spriteMask.sprite = _maskSprite;
                spriteMask.isCustomRangeActive = true;
                spriteMask.frontSortingOrder = 11 + i;
                spriteMask.backSortingOrder = 10 + i;
            }
        }

        private void Update()
        {
            // Update the scale.
            for (var i = 0; i < _layerCount - 1; i++)
            {
                var randomBaseScale = Random.Range(_baseScaleBounds.x, _baseScaleBounds.y);
                var scaleChange = _scaleChangeCurve.Evaluate(i);
                var newScale = randomBaseScale + scaleChange;
                _spriteMasks[i].transform.localScale = new Vector3(newScale, newScale, 0);
            }
            _spriteMasks[_spriteMasks.Count - 1].transform.localScale = _spriteMasks[0].transform.localScale;
        }
    }
}
