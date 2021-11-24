using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnterNameScreen : MonoBehaviour
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private GameObject _screen;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private InputChannel _inputChannel;

        [SerializeField] private float _timeBetweenCharacters;
        [SerializeField] private float _timeBetweenTextLines;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _input;
        [SerializeField] private Button _iRememberButton;
        [SerializeField] private TextMeshProUGUI _name;
        
        [SerializeField] private List<string> _textLines;
        
        private void Awake()
        {
            _text.text = "";
            _input.SetActive(false);
            _iRememberButton.SetActive(false);
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
        }

        private void ChangeLocationCompleteEvent(SceneMeta arg0, SceneMeta arg1)
        {
            if (_playerTraits.IsNameSet) return;
            
            _inputChannel.PauseInput();
            _screen.SetActive(true);

            StartCoroutine(nameof(OpeningRoutine));
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }

        IEnumerator OpeningRoutine()
        {
            foreach (var textLine in _textLines)
            {
                _text.text = "";

                foreach (var ch in textLine)
                {
                    _text.text += ch;
                    yield return new WaitForSeconds(_timeBetweenCharacters);
                }

                yield return new WaitForSeconds(_timeBetweenTextLines);
            }

            _input.SetActive(true);
            _iRememberButton.SetActive(true);
            
            yield return null;
        }

        public void IRememberClicked()
        {
            if (string.IsNullOrEmpty(_name.text)) return;

            _playerTraits.SetName(_name.text);
            _screen.SetActive(false);
            _inputChannel.ResumeInput();
        }
    }
}