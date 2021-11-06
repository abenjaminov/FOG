using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class Tab : MonoBehaviour, IPointerClickHandler
    {
        public UnityAction<string> OnTabClicked;

        public string Id;
        [SerializeField] private GameObject _tab;

        public void Show()
        {
            _tab.SetActive(true);
        }

        public void Hide()
        {
            _tab.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnTabClicked?.Invoke(Id);
        }
    }
}