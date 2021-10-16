using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class MapDetailsPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _topText;
        [SerializeField] private GameObject _anchorPoint;

        private Vector2 _placementOffset;
        
        private void Awake()
        {
            _placementOffset = transform.position - _anchorPoint.transform.position;
        }

        public void ShowMapDetails(SceneMeta sceneMeta, Vector2 position)
        {
            gameObject.SetActive(true);
            
            transform.position = position + _placementOffset + (Vector2.up);

            _topText.text = sceneMeta.AssetName;
        }
        
        public void HideMapDetails()
        {
            gameObject.SetActive(false);
        }
    }
}