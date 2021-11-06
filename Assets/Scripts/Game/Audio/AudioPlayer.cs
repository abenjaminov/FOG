using System;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using UnityEngine;

namespace Game.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Communication")]
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] private QuestsChannel _questsChannel;
        [SerializeField] private GameChannel _gameChannel;

        [Header("Sounds")]
        [SerializeField] private AudioClip _homeScreenBackgroundMusic;
        [SerializeField] private AudioClip _levelUpSFX;
        [SerializeField] private AudioClip _questCompletedSFX;
        
        
        private void Awake()
        {
            _playerChannel.LevelUpEvent += LevelUpEvent;
            _questsChannel.QuestStateChangedEvent += QuestCompleteEvent;
            _gameChannel.PlayGameEvent += PlayGameEvent;

            _audioSource.clip = _homeScreenBackgroundMusic;
            _audioSource.Play();
        }

        private void OnDestroy()
        {
            _playerChannel.LevelUpEvent -= LevelUpEvent;
            _questsChannel.QuestStateChangedEvent -= QuestCompleteEvent;
            _gameChannel.PlayGameEvent -= PlayGameEvent;
        }
        
        private void PlayGameEvent()
        {
            _audioSource.Stop();
        }
        
        private void QuestCompleteEvent(Quest quest)
        {
            if (quest.State != QuestState.PendingComplete) return;
            
            _audioSource.PlayOneShot(_questCompletedSFX);
        }
        
        private void LevelUpEvent()
        {
            _audioSource.PlayOneShot(_levelUpSFX);
        }
    }
}