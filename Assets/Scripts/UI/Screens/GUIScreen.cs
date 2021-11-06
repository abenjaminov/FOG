using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using UnityEngine;

namespace UI.Screens
{
    public abstract class GUIScreen : MonoBehaviour
    {
        [SerializeField] protected KeyboardConfiguration _keyboardConfiguration;
        [HideInInspector] public bool IsOpen;
        
        protected virtual void Awake()
        {
            UpdateUI();
        }

        public virtual void ToggleView()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            IsOpen = gameObject.activeSelf;
        }

        public abstract KeyCode GetActivationKey();
        protected abstract void UpdateUI();
    }
}