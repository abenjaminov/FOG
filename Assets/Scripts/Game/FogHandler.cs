using System;
using Assets.HeroEditor.Common.CommonScripts;
using Helpers;
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
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] private PlayerTraits _playerTraits;
        [Range(1,10)]
        [SerializeField] private int _maxLevelDiff;
        
        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
            _playerChannel.LevelUpEvent += LevelUpEvent;
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
            _playerChannel.LevelUpEvent -= LevelUpEvent;
        }

        private void LevelUpEvent()
        {
            UpdateFog();
        }

        private void ChangeLocationCompleteEvent(SceneMeta arg0, SceneMeta arg1)
        {
            UpdateFog();
        }

        private void UpdateFog()
        {
            var isLowLevel = _locationsChannel.CurrentScene.LevelAloud > _playerTraits.Level;

            if (!isLowLevel)
            {
                _fog.Stop();
                return;
            }
            else
            {
                _fog.Play();
            }

            var levelBounds = GameObject.FindGameObjectWithTag("LevelBounds");

            if (levelBounds == null)
            {
                Debug.LogError("There is no Object with LevelBounds Tag");
                return;
            }

            var levelCollider = GameObject.FindGameObjectWithTag("LevelBounds").GetComponent<BoxCollider2D>();
            var spawnPosition = levelCollider.transform.position + levelCollider.offset.ToVector3();
            var spawnSize = levelCollider.size.ToVector3();

            _fog.SetSpawnPosition(spawnPosition);
            _fog.SetSpawnBoxSize(new Vector3(spawnSize.x + 10, spawnPosition.y + 15, 0));

            var levelDiff = _locationsChannel.CurrentScene.LevelAloud - _playerTraits.Level;
            var fogAlpha = Mathf.Min(1f, (float)levelDiff / (float)_maxLevelDiff);

            _fog.SetAlpha(fogAlpha);
        }
    }
}