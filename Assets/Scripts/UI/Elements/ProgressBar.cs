using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract class ProgressBar : MonoBehaviour
    {
        private float _currentValue;
        private float _currentValueDestination;
        protected float MaxValue;

        protected float CurrentValue
        {
            get => _currentValueDestination;
            set
            {
                _currentValueDestination = value;
                StopCoroutine("UpdateCurrentValue");
                StartCoroutine(UpdateCurrentValue());
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

        private IEnumerator UpdateCurrentValue()
        {
            while (_currentValue < _currentValueDestination)
            {
                _currentValue += (Time.deltaTime * _unitsPerSecond);
                //_currentValue = Mathf.Min(1, _currentValue);
                UpdateUI();
                yield return new WaitForEndOfFrame();
            }
            
            yield return null;
        }
    }
}
