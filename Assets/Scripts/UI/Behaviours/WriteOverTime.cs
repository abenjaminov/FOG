using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WriteOverTime : MonoBehaviour
    {
        [SerializeField] private float _appearenceTime;
        private float _actualAppearenceTime;

        private Image _image;
        private static readonly int Opacity = Shader.PropertyToID("_Opacity");

        [Range(0,1)]
        public float slider;

        private Material _material;
        
        private void Awake()
        {
            _actualAppearenceTime = 0;
            _image = GetComponent<Image>();
            _material = Instantiate(_image.material);
            _image.material = _material;
        }

        private void Update()
        {
            if (_actualAppearenceTime >= _appearenceTime) return;
            
            _material.SetFloat("_Opacity", _actualAppearenceTime / _appearenceTime);
            
            _actualAppearenceTime += Time.deltaTime;
        }

        private void OnDestroy()
        {
            Destroy(_material);
        }
    }
}