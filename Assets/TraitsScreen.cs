using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TraitsScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Traits _playerTraits;

    [SerializeField] private List<GameObject> _addButtons;
    
    [SerializeField] private TextMeshProUGUI _dexText;
    [SerializeField] private TextMeshProUGUI _strText;
    [SerializeField] private TextMeshProUGUI _defText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _pointsText;

    private void Awake()
    {
        UpdateTraitsUI();
    }

    private void UpdateTraitsUI()
    {
        _dexText.SetText(_playerTraits.Dexterity.ToString());
        _strText.SetText(_playerTraits.Strength.ToString());
        _defText.SetText(_playerTraits.Defense.ToString());
        _levelText.SetText(_playerTraits.Level.ToString());
        _pointsText.SetText(_playerTraits.PointsLeft.ToString());

        if (_playerTraits.PointsLeft == 0)
        {
            foreach (var button in _addButtons)
            {
                button.SetActive(false);
            }
        }
        else
        {
            foreach (var button in _addButtons)
            {
                button.SetActive(true);
            }
        }
    }

    public void AddStrength()
    {
        _playerTraits.PointsLeft--;
        _playerTraits.Strength++;
        UpdateTraitsUI();
    }

    public void AddDexterity()
    {
        _playerTraits.PointsLeft--;
        _playerTraits.Dexterity++;
        UpdateTraitsUI();
    }
    
    public void AddDefense()
    {
        _playerTraits.PointsLeft--;
        _playerTraits.Defense++;
        UpdateTraitsUI();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
