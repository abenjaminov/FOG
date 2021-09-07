using System;
using System.Collections;
using Game;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerResistance : MonoBehaviour
    {
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private LocationsManager _locationsManager;
        [SerializeField] private PlayerTraits _playerTraits;

        private bool innerEnabled;
        
        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
        }

        private void ChangeLocationCompleteEvent(SceneMeta destination, SceneMeta source)
        {
            innerEnabled = true;
            // TODO : Update some UI about availability
            
            var levelDiff = _locationsManager.CurrentScene.LevelAloud - _playerTraits.Level;

            StopCoroutine(nameof(ReduceMonsterResistance));
            StartCoroutine(nameof(ReduceMonsterResistance));
        }

        private IEnumerator ReduceMonsterResistance()
        {
            while (_playerTraits.MonsterStateResistance >= PlayerTraits.MinMonsterStateResistance && 
                   _playerTraits.MonsterStateResistance <= PlayerTraits.MaxMonsterStateResistance)
            {
                var levelDiff = _playerTraits.Level - _locationsManager.CurrentScene.LevelAloud;
                
                yield return new WaitForSeconds(1);
                var newResistance = _playerTraits.MonsterStateResistance + levelDiff;
                _playerTraits.MonsterStateResistance = 
                    Mathf.Max(PlayerTraits.MinMonsterStateResistance, 
                        Mathf.Min(PlayerTraits.MaxMonsterStateResistance, newResistance));
            }
            
            // TODO : Reduce HP when Monster state resistance points are lost

            yield break;
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
            StopCoroutine(nameof(ReduceMonsterResistance));
        }
    }
}