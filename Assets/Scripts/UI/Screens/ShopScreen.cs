using System;
using ScriptableObjects.Channels;
using ScriptableObjects.Shops;
using UI.Shop;
using UnityEngine;

namespace UI.Screens
{
    public class ShopScreen : MonoBehaviour
    {
        [SerializeField] private NpcChannel _npcChannel;
        [SerializeField] private GameObject _shopScreen;
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private BuyShop _buyShop;
        [SerializeField] private SellShop _sellShop;

        private KeySubscription _closeSubscription;

        private void Awake()
        {
            _npcChannel.RequestOpenShopEvent += RequestOpenShopEvent;
        }

        private void OnDestroy()
        {
            _npcChannel.RequestOpenShopEvent -= RequestOpenShopEvent;
        }

        private void RequestOpenShopEvent(ShopInfo shopInfo)
        {
            //_inputChannel.PauseInput();
            _closeSubscription = _inputChannel.SubscribeKeyDown(KeyCode.Escape, () =>
            {
                //_inputChannel.ResumeInput();
                _closeSubscription.Unsubscribe();
                _shopScreen.SetActive(false);
            });
            
            _shopScreen.SetActive(true);

            _buyShop.ShopInfo = shopInfo;
            _buyShop.UpdateShop();
            _sellShop.UpdateShop();
        }
    }
}