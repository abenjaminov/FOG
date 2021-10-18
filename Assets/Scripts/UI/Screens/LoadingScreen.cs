using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.Channels;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenLoadingPhrases = 1;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private TextMeshProUGUI _loadingPhraseText;
        [SerializeField] private GameObject _loadingBackground;

        private bool _showLoading;

        private int _currentPhraseIndex = 0;
        private List<string> _loadingPhrases = new List<string>()
        {
            "Hang on",
            "Hang on.",
            "Hang on..",
            "Hang on.."
        };
        
        private List<string> _tips = new List<string>()
        {
            
        };

        private void Awake()
        {
            _locationsChannel.ChangeLocationEvent += ChangeLocationEvent;
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
            gameObject.SetActive(true);
            StartCoroutine(nameof(LoadingLoop));
        }

        private void ChangeLocationCompleteEvent(SceneMeta arg0, SceneMeta arg1)
        {
            StartCoroutine(nameof(EndLoading));
        }

        private void ChangeLocationEvent(SceneMeta arg0, SceneMeta arg1)
        {
            StartCoroutine(nameof(LoadingLoop));
        }

        IEnumerator EndLoading()
        {
            yield return new WaitForSeconds(1);
            _showLoading = false;   
            _loadingBackground.SetActive(false);
        }
        
        IEnumerator LoadingLoop()
        {
            _showLoading = true;
            _loadingBackground.SetActive(true);

            while (_showLoading)
            {
                _loadingPhraseText.text = _loadingPhrases[_currentPhraseIndex];
                _currentPhraseIndex = (_currentPhraseIndex + 1) % _loadingPhrases.Count;
                
                yield return new WaitForSeconds(_timeBetweenLoadingPhrases);
            }
        }
        
        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationEvent -= ChangeLocationEvent;
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }
    }
}