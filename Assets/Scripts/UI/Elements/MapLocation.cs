using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class MapLocation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SceneMeta _scene;
        [SerializeField] private GameObject _markerInstance;
        [SerializeField] private LocationsChannel _locationsChannel;
        
        public Action<MapLocation> MapLocationMouseEnter;
        public Action<MapLocation> MapLocationMouseExit;

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            MapLocationMouseEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MapLocationMouseExit?.Invoke(this);
        }

        public SceneMeta GetSceneMeta()
        {
            return _scene;
        }
    }
}