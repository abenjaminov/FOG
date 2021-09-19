using System;
using System.Collections;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using UnityEngine;

namespace UI.Elements
{
    public class LevelUpEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _levelUpEffect;
        [SerializeField] private GameObject _levelUpImage;
        [SerializeField] private PlayerTraits _playerTraits;

        private bool isPlayingAnimation;
        
        private void Awake()
        {
            _playerTraits.LevelUpEvent += LevelUpEvent;
        }

        private void LevelUpEvent()
        {
            StartCoroutine(nameof(PlayLevelupAnimation));
        }

        IEnumerator PlayLevelupAnimation()
        {
            isPlayingAnimation = true;
            
           _levelUpEffect.Play();
           _levelUpImage.SetActive(true);
           
           yield return new WaitForSeconds(1.4f);
           
           _levelUpImage.SetActive(false);

           isPlayingAnimation = false;
        }
        
        private void OnDestroy()
        {
            _playerTraits.LevelUpEvent -= LevelUpEvent;
        }
    }
}