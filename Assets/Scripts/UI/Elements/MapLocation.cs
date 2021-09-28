using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace UI.Elements
{
    public class MapLocation : MonoBehaviour
    {
        [SerializeField] private SceneMeta _scene;
        [SerializeField] private GameObject _markerInstance;
        [SerializeField] private LocationsChannel _locationsChannel;

        private void Awake()
        {
            _markerInstance.SetActive(false);
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationEvent;
        }

        private void OnEnable()
        {
            UpdateMarker();
        }

        private void ChangeLocationEvent(SceneMeta destination, SceneMeta source)
        {
            UpdateMarker();
        }

        private void UpdateMarker()
        {
            _markerInstance.SetActive(_locationsChannel.CurrentScene.Id == _scene.Id);
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationEvent;
        }
    }
}