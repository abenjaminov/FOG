using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WriteOverTime : MonoBehaviour
    {
        [SerializeField] private float _appearenceTime;
        [SerializeField] private float _fullyAppeardDelay;
        private float _actualAppearenceTime;

        private Image _image;
        private static readonly int OpacityProperty = Shader.PropertyToID("_Opacity");

        private Material _material;

        private int direction = 1;
        
        private void Awake()
        {
            direction = 1;
            _actualAppearenceTime = 0;
            _image = GetComponent<Image>();
            _material = Instantiate(_image.material);
            _image.material = _material;
        }

        private void Update()
        {
            _material.SetFloat(OpacityProperty, _actualAppearenceTime / _appearenceTime);

            if (_actualAppearenceTime >= _appearenceTime + _fullyAppeardDelay && direction == 1)
            {
                direction = -1;
                _actualAppearenceTime -= _fullyAppeardDelay;
            }
            else if (_actualAppearenceTime < 0)
            {
                gameObject.SetActive(false);
            }

            _actualAppearenceTime += direction * Time.deltaTime;
        }

        private void OnDestroy()
        {
            Destroy(_material);
        }
    }
}