using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class MapInfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mapName;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private PlayerChannel _playerChannel;

        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
            _playerChannel.LevelUpEvent += LevelUpEvent;
        }

        private void LevelUpEvent()
        {
            UpdateUI();
        }

        private void ChangeLocationCompleteEvent(SceneMeta dest, SceneMeta source)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_locationsChannel.CurrentScene.LevelAloud <= _playerTraits.Level)
            {
                _mapName.color = Color.yellow;
                _mapName.SetText(_locationsChannel.CurrentScene.AssetName);
            }
            else
            {
                _mapName.color = Color.red;
                _mapName.SetText(
                    $"{_locationsChannel.CurrentScene.AssetName} (Level {_locationsChannel.CurrentScene.LevelAloud} required)");
            }
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }
    }
}