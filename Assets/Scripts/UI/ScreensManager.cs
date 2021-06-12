using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using TMPro;
using UI.Screens;
using UnityEngine;

namespace Platformer.UI
{
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private GUIScreen _traitsScreen;
        [SerializeField] private GUIScreen _inventory;

        private Stack<GUIScreen> _openScreens;

        private void Awake()
        {
            _openScreens = new Stack<GUIScreen>();
            _inputChannel.RegisterKeyDown(_traitsScreen.GetActivationKey(), () => ToggleScreen(_traitsScreen));
            _inputChannel.RegisterKeyDown(_inventory.GetActivationKey(), () =>  ToggleScreen(_inventory));
            
            _inputChannel.RegisterKeyDown(KeyCode.Escape, ClosePrevScreen);
        }

        private void ToggleScreen(GUIScreen screenToToggle)
        {
            if (screenToToggle.IsOpen)
            {
                var openScreensLeft = _openScreens.Where(x => x != screenToToggle).ToList();
                _openScreens.Clear();
                foreach (var screen in openScreensLeft)
                {
                    _openScreens.Push(screen);
                }
            }
            else
            {
                _openScreens.Push(screenToToggle);
            }
            
            screenToToggle.ToggleView();    
        }

        private void ClosePrevScreen()
        {
            if (_openScreens.Count == 0) return;
            
            var prevScreen = _openScreens.Pop();
            
            ToggleScreen(prevScreen);
        }
    }
}