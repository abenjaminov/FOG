using System;
using Assets.HeroEditor.Common.CommonScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Elements
{
    public class PagingComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pagingText;
        [SerializeField] private Image _rightArrow;
        [SerializeField] private Image _leftArrow;

        public UnityAction NextPageClickedEvent;
        public UnityAction PreviousPageClickedEvent;
        
        [HideInInspector] public int CurrentPage;
        [HideInInspector] public int NumberOfPages;

        private void Awake()
        {
            CurrentPage = 1;
        }

        public void UpdateUI()
        {
            this.SetActive(NumberOfPages > 1);
            _pagingText.text = $"{CurrentPage} / {NumberOfPages}";

            if (CurrentPage == NumberOfPages)
            {
                _rightArrow.color = new Color(_rightArrow.color.r, _rightArrow.color.g, _rightArrow.color.b, 0.5f);
                _leftArrow.color = new Color(_leftArrow.color.r, _leftArrow.color.g, _leftArrow.color.b, 1);
            }
            else if (CurrentPage == 1)
            {
                _leftArrow.color = new Color(_leftArrow.color.r, _leftArrow.color.g, _leftArrow.color.b, 0.5f);
                _rightArrow.color = new Color(_rightArrow.color.r, _rightArrow.color.g, _rightArrow.color.b, 1);
            }
        }
        
        public void NextPage()
        {
            CurrentPage = Mathf.Min(NumberOfPages, CurrentPage + 1);
            NextPageClickedEvent?.Invoke();
        }
        
        public void PreviousPage()
        {
            CurrentPage = Mathf.Max(1, CurrentPage - 1);
            PreviousPageClickedEvent?.Invoke();
        }
    }
}