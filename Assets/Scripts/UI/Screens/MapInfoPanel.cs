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

        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
        }

        private void ChangeLocationCompleteEvent(SceneMeta dest, SceneMeta source)
        {
            if (_locationsChannel.CurrentScene.LevelAloud <= _playerTraits.Level)
            {
                _mapName.color = Color.yellow;
                _mapName.SetText(dest.AssetName);
            }
            else
            {
                _mapName.color = Color.red;
                _mapName.SetText($"{dest.AssetName} (Level {_locationsChannel.CurrentScene.LevelAloud} required)");
            }
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }
    }
}