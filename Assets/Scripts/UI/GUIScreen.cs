using ScriptableObjects.Channels;
using UnityEngine;

namespace UI
{
    public abstract class GUIScreen : MonoBehaviour
    {
        [SerializeField] protected InputChannel _inputChannel;
        protected KeyCode activationKey;
        
        protected virtual void Awake()
        {
            _inputChannel.RegisterKeyDown(GetActivationKey(), ToggleView);
            this.ToggleView();
            UpdateUI();
        }

        private void ToggleView()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }

        protected abstract KeyCode GetActivationKey();
        protected abstract void UpdateUI();
    }
}