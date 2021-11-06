using System;
using System.Collections.Generic;
using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class SettingsScreen : GUIScreen
    {
        [SerializeField] private List<Tab> _tabs;
        private string selectTabId;
        
        protected override void Awake()
        {
            foreach (var tab in _tabs)
            {
                tab.OnTabClicked += OnTabClicked;
            }
            
            if(_tabs.Count > 0)
                selectTabId = _tabs[0].Id;
            
            base.Awake();
        }

        private void OnDestroy()
        {
            foreach (var tab in _tabs)
            {
                tab.OnTabClicked -= OnTabClicked;
            }
        }

        private void OnTabClicked(string id)
        {
            selectTabId = id;
            UpdateUI();
        }

        public override KeyCode GetActivationKey()
        {
            return _keyboardConfiguration.OpenSettingsScreen;
        }

        protected override void UpdateUI()
        {
            ShowTab(selectTabId);
        }

        private void ShowTab(string id)
        {
            foreach (var tab in _tabs)
            {
                if(tab.Id == id)
                    tab.Show();
                else
                    tab.Hide();
            }
        }
    }
}