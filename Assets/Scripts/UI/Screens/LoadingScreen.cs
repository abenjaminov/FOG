using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.Channels;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Screens
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenLoadingPhrases = 1;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private TextMeshProUGUI _loadingPhraseText;
        [SerializeField] private TextMeshProUGUI _tipsText;
        [SerializeField] private GameObject _loadingBackground;
        [SerializeField] private float _timeBetweenTips;

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
            "Remove an item from a hotkey by pressing the right click when over it.",
            "Drop an item from your inventory by pressing right click while hovering above it.",
            "Drop off ladders pressing the JUMP key."
        };

        private void Awake()
        {
            _locationsChannel.ChangeLocationEvent += ChangeLocationEvent;
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
            StartLoading();
        }

        private void ChangeLocationCompleteEvent(SceneMeta arg0, SceneMeta arg1)
        {
            _showLoading = false;
        }

        private void ChangeLocationEvent(SceneMeta arg0, SceneMeta arg1)
        {
            StartLoading();
        }

        private void StartLoading()
        {
            _showLoading = true;
            StartCoroutine(nameof(LoadingLoop));
            StartCoroutine(nameof(TipsLoop));
        }

        IEnumerator LoadingLoop()
        {
            _loadingBackground.SetActive(true);

            while (_showLoading)
            {
                _loadingPhraseText.text = _loadingPhrases[_currentPhraseIndex];
                _currentPhraseIndex = (_currentPhraseIndex + 1) % _loadingPhrases.Count;
                
                yield return new WaitForSeconds(_timeBetweenLoadingPhrases);
            }
            
            _loadingBackground.SetActive(false);
        }

        IEnumerator TipsLoop()
        {
            var arrayLength = _tips.Count;

            while (_showLoading)
            {
                var randomTipIndex = Random.Range(0, arrayLength);

                _tipsText.text = _tips[randomTipIndex];
            
                (_tips[arrayLength - 1], _tips[randomTipIndex]) = (_tips[randomTipIndex], _tips[arrayLength - 1]);
            
                arrayLength = (arrayLength - 1 + _tips.Count) % _tips.Count + 1;

                yield return new WaitForSeconds(_timeBetweenTips);
            }
        }
        
        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationEvent -= ChangeLocationEvent;
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }
    }
}