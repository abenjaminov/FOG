using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class MapInfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mapName;
        [SerializeField] private LocationsChannel _locationsChannel;

        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
        }

        private void ChangeLocationCompleteEvent(SceneMeta dest, SceneMeta source)
        {
            _mapName.SetText(dest.AssetName);
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }
    }
}