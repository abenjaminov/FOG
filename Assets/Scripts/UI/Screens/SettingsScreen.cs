using System;
using System.Collections.Generic;
using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class SettingsScreen : MonoBehaviour
    {
        [SerializeField] private List<Tab> _tabs;

        public void Awake()
        {
            ShowTab(0);
        }

        private void ShowTab(int tabIndex)
        {
            for (var i = 0; i < _tabs.Count; i++)
            {
                if(i == tabIndex)
                    _tabs[i].Show();
                else
                    _tabs[i].Hide();
            }
        }
    }
}