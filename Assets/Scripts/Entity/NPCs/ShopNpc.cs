using ScriptableObjects.Shops;
using UnityEngine;

namespace Entity.NPCs
{
    public class ShopNpc : Npc
    {
        [SerializeField] private ShopInfo _shopInfo;
        
        public override void HandleDoubleClick()
        {
            _npcChannel.OpenShopRequest(_shopInfo);
        }
    }
}