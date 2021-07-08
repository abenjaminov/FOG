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
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            var myMaterial = Instantiate(_image.material);
            _image.material = myMaterial;
        }

        private void Update()
        {
            if (_actualAppearenceTime >= _appearenceTime) return;
            
            //_image.material.SetFloat(Opacity, _actualAppearenceTime / _appearenceTime);
            _image.material.SetFloat("_Opacity", slider);
            
            _actualAppearenceTime += Time.deltaTime;
        }
    }
}