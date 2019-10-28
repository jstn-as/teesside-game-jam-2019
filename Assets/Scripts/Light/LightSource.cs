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
        [SerializeField] private bool _destroyOnDie;
        [SerializeField] private AnimationCurve _scaleChangeCurve;
        [SerializeField] private Sprite _maskSprite;
        private List<SpriteMask> _spriteMasks;

        public void SetLifetime(float newLifetime)
        {
            _lifetime = newLifetime;
        }
        public float GetLightRadius()
        {
            var dropOff = _scaleChangeCurve.Evaluate(_layerCount);
            return _baseLight + _flicker + dropOff;
        } 
        private void Awake()
        {
            _spriteMasks = new List<SpriteMask>();
            // Create the sprite masks.
            for (var i = 0; i < _layerCount; i++)
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
            // Add itself to the light sources.
            FindObjectOfType<ShadowCaster>().AddLight(this);
        }

        private void Update()
        {
            // Update the lifetime of the light.
            if (_lifetime > 0)
            {
                _lifetime -= Time.deltaTime;
            }
            else if (_lifetime < 0 && _destroyOnDie)
            {
                Destroy(gameObject, 2f);
            }
            // Update the scale.
            for (var i = 0; i < _layerCount - 1; i++)
            {
                var randomBaseScale = Random.Range(_baseLight, _baseLight + _flicker);
                var scaleChange = _scaleChangeCurve.Evaluate(i);
                var newScale = randomBaseScale + scaleChange;
                var life = Mathf.Clamp(_lifetime, 0, _baseLight);
                newScale *= (life * _lifetimeImpact);
                _spriteMasks[i].transform.localScale = new Vector3(newScale, newScale, 0);
            }
        }

        private void OnDestroy()
        {
            FindObjectOfType<ShadowCaster>().RemoveLight(this);
        }
    }
}
