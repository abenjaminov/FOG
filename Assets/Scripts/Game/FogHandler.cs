using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using UnityEngine;
using UnityEngine.VFX;

namespace Game
{
    public class FogHandler : MonoBehaviour
    {
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private Fog _fog;
        [SerializeField] private PlayerTraits _playerTraits;
        [Range(1,10)]
        [SerializeField] private int _maxLevelDiff;
        
        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
            _fog.SetActive(false);
        }

        private void ChangeLocationCompleteEvent(SceneMeta arg0, SceneMeta arg1)
        {
            var isLowLevel = _locationsChannel.CurrentScene.LevelAloud > _playerTraits.Level;
            _fog.SetActive(isLowLevel);

            if (!isLowLevel) return;
            
            var levelDiff = _locationsChannel.CurrentScene.LevelAloud - _playerTraits.Level;
            var fogAlpha = Mathf.Min(1f, (float)levelDiff / (float)_maxLevelDiff);
            
            _fog.SetAlpha(fogAlpha);
        }
    }
}