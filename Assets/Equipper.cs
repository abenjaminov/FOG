using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

public class Equipper : MonoBehaviour
{
    [SerializeField] private SpriteCollection _spriteCollection;
    [SerializeField] private Assets.HeroEditor.Common.CharacterScripts.Character _character;
    
    // Start is called before the first frame update
    void Start()
    {
        _character.Equip(_spriteCollection.Armor[0], EquipmentPart.Armor);
        _character.Equip(_spriteCollection.Bow[0], EquipmentPart.Bow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
