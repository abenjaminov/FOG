using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract class ProgressBar : MonoBehaviour
    {
        protected float MaxValue;
        protected  float CurrentValue;

        [SerializeField] protected  RectTransform _background;
        [SerializeField] protected RectTransform _foreground;
        [SerializeField] protected  TextMeshProUGUI _progressText;
        
        protected virtual void UpdateUI()
        {
            var actualPercentage = CurrentValue / MaxValue;

            var foregroundWidth = _background.rect.width * Mathf.Min(actualPercentage,1f);
            _foreground.sizeDelta = new Vector2(foregroundWidth, _foreground.rect.height);
            
            _progressText.SetText(CurrentValue + " / " + MaxValue);
        }
    }
}
