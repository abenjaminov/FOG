using System;
using System.Collections.Generic;
using System.Linq;
using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class MapScreen : GUIScreen
    {
        private List<MapLocation> _mapLocations;
        [SerializeField] private MapDetailsPanel _mapDetailsPanel;

        protected override void Awake()
        {
            base.Awake();
            
            _mapLocations = GetComponentsInChildren<MapLocation>().ToList();
            
            foreach (var itemView in _mapLocations)
            {
                itemView.MapLocationMouseEnter += MapLocationMouseEnter;
                itemView.MapLocationMouseExit += MapLocationMouseExit;
            }
        }

        private void MapLocationMouseExit(MapLocation mapLocation)
        {
            _mapDetailsPanel.HideMapDetails();
        }

        private void MapLocationMouseEnter(MapLocation mapLocation)
        {
            _mapDetailsPanel.ShowMapDetails(mapLocation.GetSceneMeta(), mapLocation.transform.position);
        }

        public override KeyCode GetActivationKey()
        {
            return _keyboardConfiguration.OpenMapScreen;
        }

        protected override void UpdateUI()
        {
            
        }

        public override void ToggleView()
        {
            base.ToggleView();

            if (!IsOpen)
            {
                _mapDetailsPanel.HideMapDetails();
            }
        }

        private void OnDestroy()
        {
            foreach (var itemView in _mapLocations)
            {
                itemView.MapLocationMouseEnter -= MapLocationMouseEnter;
                itemView.MapLocationMouseExit -= MapLocationMouseExit;
            }
        }
    }
}