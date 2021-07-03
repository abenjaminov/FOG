using Assets.HeroEditor.Common.CommonScripts;
using Platformer;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.Enemies
{
    public class EnemyHealthUI : MonoBehaviour, IHealthUI
    {
        [SerializeField] private Image _healthValueImage;
        [SerializeField] private Canvas _UI;

        private float _fullWidth;

        void Awake()
        {
            _fullWidth = _healthValueImage.rectTransform.rect.width;
        }
        
        public void SetHealth(float percentage)
        {
            var newWidth = _fullWidth * percentage;
            _healthValueImage.rectTransform.sizeDelta =
                new Vector2(newWidth, _healthValueImage.rectTransform.rect.height);
        }

        public void ToggleUI(bool isActive)
        {
            this._UI.SetActive(isActive);
        }
    }
}