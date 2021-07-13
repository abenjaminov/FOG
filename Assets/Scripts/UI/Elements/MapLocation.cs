using System;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace UI.Elements
{
    public class MapLocation : MonoBehaviour
    {
        [SerializeField] private SceneAsset _scene;
        [SerializeField] private GameObject _markerInstance;
        [SerializeField] private LocationsChannel _locationsChannel;

        private void Awake()
        {
            _markerInstance.SetActive(false);
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationEvent;
        }

        private void ChangeLocationEvent(SceneAsset destination, SceneAsset source)
        {
            if (source == _scene)
            {
                _markerInstance.SetActive(false);
            }
            else if (destination == _scene)
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