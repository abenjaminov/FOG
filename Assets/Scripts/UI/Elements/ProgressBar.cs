using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract class ProgressBar : MonoBehaviour
    {
        protected float MaxValue;
        protected  float CurrentValue;

        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _foreground;
        [SerializeField] private TextMeshProUGUI _progressText;
        
        protected virtual void UpdateUI()
        {
            var actualPercentage = CurrentValue / MaxValue;

            var foregroundWidth = _background.rect.width * actualPercentage;
            _foreground.sizeDelta = new Vector2(foregroundWidth, _foreground.rect.height);
            
            _progressText.SetText(CurrentValue + " / " + MaxValue);
        }
    }
}
