using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Light
{
    public class LightSource : MonoBehaviour
    {
        [SerializeField] private float _baseLight = 1;
        [SerializeField] private float _flicker = 0.4f;
        [SerializeField] private float _lifetime;
        [SerializeField] private float _lifetimeImpact = 1;
        [SerializeField] private int _layerCount;
        [SerializeField] private AnimationCurve _scaleChangeCurve;
        [SerializeField] private Sprite _maskSprite;
        [SerializeField] private List<SpriteMask> _spriteMasks;

        public float GetLightRadius()
        {
            var dropOff = _scaleChangeCurve.Evaluate(_layerCount);
            return _baseLight + _flicker + dropOff;
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
            // Update the lifetime of the light.
            if (_lifetime > 0)
            {
                _lifetime -= Time.deltaTime;
                _baseLight = Mathf.Clamp(_baseLight, 0, _lifetime * _lifetimeImpact);
                _flicker = Mathf.Clamp(_flicker, 0, _lifetime * _lifetimeImpact);
            }
            else if (_lifetime < 0)
            {
                Destroy(gameObject);
            }
            // Update the scale.
            for (var i = 0; i < _layerCount - 1; i++)
            {
                var randomBaseScale = Random.Range(_baseLight, _baseLight + _flicker);
                var scaleChange = _scaleChangeCurve.Evaluate(i);
                var newScale = randomBaseScale + scaleChange;
                _spriteMasks[i].transform.localScale = new Vector3(newScale, newScale, 0);
            }
            _spriteMasks[_spriteMasks.Count - 1].transform.localScale = _spriteMasks[0].transform.localScale;
        }
    }
}
