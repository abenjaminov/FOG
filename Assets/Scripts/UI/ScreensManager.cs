using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using UI.Screens;
using UnityEngine;

namespace Platformer.UI
{
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private GUIScreen _traitsScreen;
        [SerializeField] private GUIScreen _inventory;

        private void Awake()
        {
            _inputChannel.RegisterKeyDown(_traitsScreen.GetActivationKey(), _traitsScreen.ToggleView);
            _inputChannel.RegisterKeyDown(_inventory.GetActivationKey(), _inventory.ToggleView);
        }
    }
}