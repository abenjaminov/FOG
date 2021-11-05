using ScriptableObjects.Channels;
using UnityEngine;

namespace UI.Screens
{
    public class HomeScreen : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;
        
        public void OnPlayGameCLicked()
        {
            _gameChannel.OnPlayGame();     
        }
        
        public void OnSettingsClicked()
        {
            
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}