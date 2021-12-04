using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Channels.Info;
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
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private CombatChannel _combatChannel;

        [Header("Sounds")]
        [SerializeField] private AudioClip _homeScreenBackgroundMusic;
        [SerializeField] private AudioClip _levelUpSFX;
        [SerializeField] private AudioClip _questCompletedSFX;
        [SerializeField] private AudioClip _usePotionSFX;
        [SerializeField] private AudioClip _playerJumpSFX;
        [SerializeField] private AudioClip _insufficientFundsSFX;
        [SerializeField] private AudioClip _usedCoinsSFX;

        private void Awake()
        {
            _playerChannel.PlayerJumpEvent += PlayerJumpEvent;
            _playerChannel.LevelUpEvent += LevelUpEvent;
            _questsChannel.QuestStateChangedEvent += QuestCompleteEvent;
            _gameChannel.PlayGameEvent += PlayGameEvent;
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
            _inventoryChannel.UsedPotionEvent += UsedPotionEvent;
            _combatChannel.EnemyHitEvent += EnemyHitEvent;
            _combatChannel.UseAbilityEvent += UseAbilityEvent;
            _inventoryChannel.InsufficientFundsEvent += InsufficientFundsEvent;
            _inventoryChannel.UsedCoinsEvent += UsedCoinsEvent;

            PlayBackgroundClip(_homeScreenBackgroundMusic);
        }

        private void OnDestroy()
        {
            _playerChannel.LevelUpEvent -= LevelUpEvent;
            _questsChannel.QuestStateChangedEvent -= QuestCompleteEvent;
            _gameChannel.PlayGameEvent -= PlayGameEvent;
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
            _playerChannel.PlayerJumpEvent -= PlayerJumpEvent;
            _combatChannel.EnemyHitEvent -= EnemyHitEvent;
            _combatChannel.UseAbilityEvent -= UseAbilityEvent;
            _inventoryChannel.InsufficientFundsEvent -= InsufficientFundsEvent;
            _inventoryChannel.UsedCoinsEvent -= UsedCoinsEvent;
        }
        
        private void UsedCoinsEvent()
        {
            _audioSource.PlayOneShot(_usedCoinsSFX, 3);
        }
        
        private void InsufficientFundsEvent()
        {
            _audioSource.PlayOneShot(_insufficientFundsSFX, 3);
        }
        
        private void UseAbilityEvent(UseAbilityEventInfo useAbilityEventInfo)
        {
            if (useAbilityEventInfo.Weapon.HitSFX == null) return;
            
            _audioSource.PlayOneShot(useAbilityEventInfo.Weapon.UseSFX, 3);
        }
        
        private void EnemyHitEvent(EnemyHitEventInfo enemyHitEventInfo)
        {
            if (enemyHitEventInfo.Weapon.HitSFX == null) return;
            
            _audioSource.PlayOneShot(enemyHitEventInfo.Weapon.HitSFX, 3);
        }

        private void PlayerJumpEvent()
        {
            _audioSource.PlayOneShot(_playerJumpSFX);
        }

        private void UsedPotionEvent()
        {
            _audioSource.PlayOneShot(_usePotionSFX);
        }

        private void PlayBackgroundClip(AudioClip clip)
        {
            _audioSource.Stop();
            
            if (clip == null) return;
            
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        
        private void ChangeLocationCompleteEvent(SceneMeta destination, SceneMeta source)
        {
            PlayBackgroundClip(destination.SceneAudio);
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