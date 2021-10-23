using System;
using Entity.Enemies;
using HeroEditor.Common.Enums;
using ScriptableObjects;
using ScriptableObjects.Inventory;
using ScriptableObjects.Traits;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Helpers
{
    public static class TraitsHelper
    {
        private static float MainStatMultiplier = 1/75f;
        private static float SecondaryStatMultiplier = 1/150f;

        public static int GetPlayerMaxHealth(PlayerTraits traits)
        {
            return 100 + (35 * (traits.Level - 1)) + (7 * (traits.Level - 1)) +
                          (traits.Constitution - 5) * traits.Level;
        }
        
        private static int GetMaxBaseDamage(int level)
        {
            var minBase = GetMinBaseDmg(level);
            return (minBase + (2*minBase/level));
        }
        private static int GetMinBaseDmg(int level)
        {
            return (int) (1 + Mathf.Pow(level, 2) * Mathf.Log10(level));
        }

        private static int GetWeaponDamageAddition(PlayerTraits traits, PlayerEquipment equipment)
        {
            var result = equipment.PrimaryWeapon.MonsterResistance * traits.Level;
            return result;
        }
        
        public static int GetMinPlayerDamage(PlayerTraits traits, PlayerEquipment equipment)
        {
            var weaponAddition = GetWeaponDamageAddition(traits, equipment);
            
            int minTraitsDmg = 0;

            if (equipment.PrimaryWeapon.Part == EquipmentPart.MeleeWeapon1H)
            {
                minTraitsDmg = GetMeleeMin(traits);
            }
            else if (equipment.PrimaryWeapon.Part == EquipmentPart.Bow)
            {
                minTraitsDmg = GetBowMin(traits);
            }
            
            var minFinal = minTraitsDmg + weaponAddition;
            return minFinal;
        }
        
        public static int GetMaxPlayerDamage(PlayerTraits traits, PlayerEquipment equipment)
        {
            var weaponAddition = GetWeaponDamageAddition(traits, equipment);
            
            int maxTraitsDmg = 0;

            if (equipment.PrimaryWeapon.Part == EquipmentPart.MeleeWeapon1H)
            {
                maxTraitsDmg = GetMeleeMax(traits);
            }
            else if (equipment.PrimaryWeapon.Part == EquipmentPart.Bow)
            {
                maxTraitsDmg = GetBowMax(traits);
            }
            
            var minFinal = maxTraitsDmg + weaponAddition;
            return minFinal;
        }
        
        public static int CalculateDamageInflictedByPlayer(PlayerTraits traits, PlayerEquipment equipment)
        {
            var minFinal = GetMinPlayerDamage(traits, equipment);
            var maxFinal = GetMaxPlayerDamage(traits, equipment);
            
            // + 1 because max is Exclusive
            return Random.Range(minFinal, maxFinal + 1);
        }

        #region Melee

        private static int GetMeleeMin(PlayerTraits traits)
        {
            var minBase = GetMinBaseDmg(traits.Level);

            return MeleeFormula(minBase, traits);
        }
        
        private static int GetMeleeMax(PlayerTraits traits)
        {
            var maxBase = GetMaxBaseDamage(traits.Level);

            return MeleeFormula(maxBase, traits);
        }

        private static int MeleeFormula(int baseFactor, PlayerTraits traits)
        {
            return (int) Mathf.Ceil(baseFactor +
                                    (traits.Strength * MainStatMultiplier * baseFactor) +
                                    (traits.Dexterity * SecondaryStatMultiplier * baseFactor));
        }

        #endregion
        
        #region Bow

        private static int GetBowMin(PlayerTraits traits)
        {
            var minBase = GetMinBaseDmg(traits.Level);

            return BowFormula(minBase, traits);
        }
        
        private static int GetBowMax(PlayerTraits traits)
        {
            var maxBase = GetMaxBaseDamage(traits.Level);

            return MeleeFormula(maxBase, traits);
        }

        private static int BowFormula(int baseFactor, PlayerTraits traits)
        {
            return (int) Mathf.Ceil(baseFactor +
                                    (traits.Dexterity * MainStatMultiplier * baseFactor) +
                                    (traits.Strength * SecondaryStatMultiplier * baseFactor));
        }

        #endregion

        public static float CalculateAttackRange(PlayerTraits attacker)
        {
            // TODO: Turn into ScriptableObject?
            var minRange = 4;
            var maxRange = 15;
            var maxDex = 125;

            var range = Mathf.Min(maxRange, minRange + (attacker.Dexterity / (maxRange * maxDex)));
            return range;
        }

        #region Enemies

        public static int GetEnemyDamage(Enemy enemy)
        {
            var minFactor = 3;
            var maxFactor = 5;

            var minValue = CalculateEnemyAttackValue(enemy, minFactor);
            var maxValue = CalculateEnemyAttackValue(enemy, maxFactor);

            var damage = Random.Range(minValue, maxValue + 1);

            return damage;
        }

        private static int CalculateEnemyAttackValue(Enemy enemy,int factor)
        {
            var result = (int)Math.Ceiling(factor * Mathf.Pow(enemy.Traits.Level + 1, 1.5f));
            return result;
        }

        #endregion
    }
}