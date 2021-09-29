using UnityEngine;

namespace UI.Screens
{
    public class MapScreen : GUIScreen
    {
        public override KeyCode GetActivationKey()
        {
            return _keyboardConfiguration.OpenMapScreen;
        }

        protected override void UpdateUI()
        {
            
        }
    }
}