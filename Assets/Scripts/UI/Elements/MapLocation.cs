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

        private void ChangeLocationEvent(SceneMeta destination, SceneMeta source)
        {
            if (source.Id == _scene.Id)
            {
                _markerInstance.SetActive(false);
            }
            else if (destination.Id == _scene.Id)
            {
                _markerInstance.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationEvent;
        }
    }
}