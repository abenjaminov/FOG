using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract class ProgressBar : MonoBehaviour
    {
        private float _defaultStartingValue = -1;
        private float _currentValue  = -1;
        private float _currentValueDestination;
        protected float MaxValue;
        private bool _isUpdatingValue;
        
        protected float CurrentValue
        {
            get => _currentValueDestination;
            set
            {
                _currentValueDestination = value;

                if (!_isUpdatingValue)
                {
                    StartCoroutine(nameof(UpdateCurrentValue));
                }
                    
            }
        }

        [SerializeField] protected  RectTransform _background;
        [SerializeField] protected RectTransform _foreground;
        [SerializeField] protected  TextMeshProUGUI _progressText;
        [SerializeField] private float _unitsPerSecond = 2;

        protected virtual void UpdateUI()
        {
            var actualPercentage = _currentValue / MaxValue;

            var foregroundWidth = _background.rect.width * Mathf.Min(actualPercentage,1f);
            
            _foreground.sizeDelta = new Vector2(foregroundWidth, _foreground.rect.height);
            
            _progressText.SetText(CurrentValue + " / " + MaxValue);
        }

        protected void SetInitialCurrentValue(float value)
        {
            _currentValue = value;
            _currentValueDestination = value;
        }
        
        private IEnumerator UpdateCurrentValue()
        {
            _isUpdatingValue = true;

            while (Math.Abs(_currentValue - _currentValueDestination) > 0.01f)
            {
                if(_currentValue > _currentValueDestination)
                    _currentValue = Mathf.Max(_currentValue -(Time.deltaTime * _unitsPerSecond), 
                                              _currentValueDestination);
                else
                    _currentValue = Mathf.Min(_currentValue + (Time.deltaTime * _unitsPerSecond), 
                                              _currentValueDestination);
                
                UpdateUI();
                yield return new WaitForEndOfFrame();
            }    
            

            _isUpdatingValue = false;
            yield return null;
        }
    }
}
