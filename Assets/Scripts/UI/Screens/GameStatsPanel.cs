using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using TMPro;
using UnityEngine;

public class GameStatsPanel : MonoBehaviour
{
    [SerializeField] private PlayerTraits _playerTraits;
    [SerializeField] private PlayerChannel _playerChannel;
    [SerializeField] private PersistenceChannel _persistenceChannel;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    
    void Awake()
    {
        UpdateLevelText();
        _playerChannel.LevelUpEvent += LevelUpEvent;
        _playerChannel.NameSetEvent += NameSetEvent;
        _persistenceChannel.GameModulesLoadedEvent += GameModulesLoadedEvent;
    }

    private void GameModulesLoadedEvent()
    {
        _nameText.SetText(_playerTraits.Name);
    }

    private void NameSetEvent()
    {
        _nameText.SetText(_playerTraits.Name);
    }

    private void UpdateLevelText()
    {
        _levelText.SetText("Level " + _playerTraits.Level);
    }

    private void LevelUpEvent()
    {
        UpdateLevelText();
    }

    private void OnDestroy()
    {
        _playerChannel.LevelUpEvent -= LevelUpEvent;
        _playerChannel.NameSetEvent -= NameSetEvent;
        _persistenceChannel.GameModulesLoadedEvent -= GameModulesLoadedEvent;
    }
}
