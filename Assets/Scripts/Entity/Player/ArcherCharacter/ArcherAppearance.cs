using HeroEditor.Common.Enums;

namespace Entity.Player.ArcherCharacter
{
    public class ArcherAppearance : PlayerAppearance
    {
        protected override void Awake()
        {
            base.Awake();
            
            _character.Equip(_playerEquipment.PrimaryWeapon.Item, EquipmentPart.Bow);
        }
    }
}