using ScriptableObjects.Channels;
using UnityEngine;

namespace UI.Screens
{
    public abstract class GUIScreen : MonoBehaviour
    {
        [SerializeField] protected InputChannel _inputChannel;
        protected KeyCode activationKey;
        
        protected virtual void Awake()
        {
            UpdateUI();
        }

        public void ToggleView()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }

        public abstract KeyCode GetActivationKey();
        protected abstract void UpdateUI();
    }
}