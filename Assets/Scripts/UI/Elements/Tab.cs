using UnityEngine;

namespace UI.Elements
{
    public class Tab : MonoBehaviour
    {
        [SerializeField] private GameObject _tab;

        public void Show()
        {
            _tab.SetActive(true);
        }

        public void Hide()
        {
            _tab.SetActive(false);
        }
    }
}