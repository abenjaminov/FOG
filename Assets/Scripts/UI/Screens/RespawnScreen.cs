using System;
using System.Collections;
using Assets.HeroEditor.Common.CommonScripts;
using Game;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class RespawnScreen : MonoBehaviour
    {
        [SerializeField] private int _timeBeforeRespawn;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TextMeshProUGUI _respawnTimer;
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private GameObject _screenVisuals;

        private int _actualTimeBeforeRespawn;

        private void Awake()
        {
            _playerTraits.DiedEvent += PlayerDied;
            _screenVisuals.SetActive(false);
        }

        private void OnDestroy()
        {
            _playerTraits.DiedEvent -= PlayerDied;
        }

        private void PlayerDied()
        {
            _actualTimeBeforeRespawn = _timeBeforeRespawn;
            _respawnTimer.text = _timeBeforeRespawn.ToString();
            
            StartCoroutine(nameof(RespawnSequence));
        }

        private IEnumerator RespawnSequence()
        {
            _screenVisuals.SetActive(true);
            
            yield return new WaitForSeconds(1);
            
            while (_actualTimeBeforeRespawn > 0)
            {
                _respawnTimer.text = _actualTimeBeforeRespawn.ToString();
                yield return new WaitForSeconds(1);
                _actualTimeBeforeRespawn--;
            }
            
            _screenVisuals.SetActive(false);
            
            _playerTraits.Revive();
            _locationsChannel.OnRespawn(_locationsChannel.CurrentScene.RespawnScene, _locationsChannel.CurrentScene);
        }
    }
}