using System;
using System.Collections;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HpProgressBar : ProgressBar
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] private Image _progressImage;
        [SerializeField] private Color _alternateColor;
        [SerializeField] private AudioSource _audioSource;
        [Range(0,1)] [SerializeField] private float _warningPercentage;
        private bool _flashHealth;
        
        private void Awake()
        {
            MaxValue = _playerTraits.MaxHealth;
            CurrentValue = _playerTraits.GetCurrentHealth();
            
            _playerTraits.HealthChangedEvent += UpdateUI;
            _playerChannel.LevelUpEvent += UpdateUI;
            _playerChannel.TraitsChangedEvent += UpdateUI;
            
            UpdateUI();
        }

        protected override void UpdateUI()
        {
            MaxValue = _playerTraits.MaxHealth;
            CurrentValue = _playerTraits.GetCurrentHealth();
            base.UpdateUI();

            if (!_flashHealth && CurrentValue <= (MaxValue * _warningPercentage))
            {
                _flashHealth = true;
                
                StartCoroutine(nameof(FlashHealth));
            }
            else if (_flashHealth && CurrentValue > (MaxValue * _warningPercentage))
            {
                _flashHealth = false;
            }
        }

        private IEnumerator FlashHealth()
        {
            yield return new WaitForEndOfFrame();
            _audioSource.Play();
            
            while (_flashHealth)
            {
                yield return new WaitForSeconds(0.565f);
                _progressImage.color = _progressImage.color == Color.red ? _alternateColor : Color.red;
            }

            _progressImage.color = Color.red;
            
            _audioSource.Stop();
        }
        
        private void OnDestroy()
        {
            _playerTraits.HealthChangedEvent -= UpdateUI;
            _playerChannel.LevelUpEvent -= UpdateUI;
            _playerChannel.TraitsChangedEvent -= UpdateUI;
        }
    }
}