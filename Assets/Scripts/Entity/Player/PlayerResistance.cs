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
        [SerializeField] private PlayerTraits _playerTraits;

        private bool innerEnabled;
        
        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
        }

        private void ChangeLocationCompleteEvent(SceneMeta destination, SceneMeta source)
        {
            innerEnabled = false;
            
            StartCoroutine(nameof(ReduceMonsterResistance));
        }

        private IEnumerator ReduceMonsterResistance()
        {
            innerEnabled = true;
            var levelDiff = _playerTraits.Level - _locationsChannel.CurrentScene.LevelAloud;
            var delta = Mathf.Sign(levelDiff) * Mathf.Pow(levelDiff, 2);
            
            while (innerEnabled)
            {
                yield return new WaitForSeconds(2);

                if (_playerTraits.MonsterStateResistance > PlayerTraits.MinMonsterStateResistance)
                {
                    var newResistance = _playerTraits.MonsterStateResistance + delta;
                    _playerTraits.MonsterStateResistance = 
                        Mathf.Max(PlayerTraits.MinMonsterStateResistance, 
                            Mathf.Min(PlayerTraits.MaxMonsterStateResistance, newResistance));
                }
                else if(delta < 0)
                {
                    _playerTraits.ChangeCurrentHealth(delta);
                }
            }
            
            yield return null;
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
            StopCoroutine(nameof(ReduceMonsterResistance));
        }
    }
}