using System;
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
        
        private static int GetMaxBaseDamage(int level)
        {
            var minBase = GetMinBaseDmg(level);
            return (minBase + (2*minBase/level));
        }
        private static int GetMinBaseDmg(int level)
        {
            return (int) (1 + Mathf.Pow(level, 2) * Mathf.Log10(level));
        }

        public static int CalculatePlayerDamage(PlayerTraits traits, PlayerEquipment equipment)
        {
            int minTraitsDmg = 0;
            var maxTraitsDmg = 0;

            if (equipment.PrimaryWeapon.Part == EquipmentPart.MeleeWeapon1H)
            {
                minTraitsDmg = GetMeleeMin(traits);
                maxTraitsDmg = GetMeleeMax(traits);
            }
            else if (equipment.PrimaryWeapon.Part == EquipmentPart.Bow)
            {
                minTraitsDmg = GetBowMin(traits);
                maxTraitsDmg = GetBowMax(traits);
            }

            var weaponAddition = equipment.PrimaryWeapon.MonsterResistance * traits.Level;

            var minFinal = minTraitsDmg + weaponAddition;
            var maxFinal = maxTraitsDmg + weaponAddition;
            
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
        
        public static int CalculateDamage(Traits attacker, Traits receiver)
        {
            // var rangeDiff = GetRangeDiff(attacker);
            //
            // return CalculateDamage(attacker, receiver, rangeDiff);
            return 0;
        }

        private static float GetRangeDiff(Traits attacker)
        {
            // var rangeDiff = Mathf.Exp(Mathf.Ceil((float) attacker.Level / attacker.Dexterity));
            //
            // return rangeDiff;
            return 0;
        }

        private static int CalculateDamage(Traits attacker, Traits receiver,float rangeDiff)
        {
            var rangeValue = Mathf.Ceil(Random.Range(-rangeDiff, rangeDiff));

            return CalculateAttackerDamage(attacker, rangeValue);
        }

        private static int CalculateAttackerDamage(Traits attacker, float rangeValue)
        {
            // var damage = Mathf.Ceil(attacker.Strength * ((float) attacker.Level / LevelConfiguration.MAXLevel));
            // damage += rangeValue;
            //
            // return
            // (int) Mathf.Ceil(Mathf.Max(attacker.Level, attacker.Level + damage));
            return 0;
        }

        public static int GetMaxDamage(Traits attacker)
        {
            var rangeDiff = GetRangeDiff(attacker);

            return CalculateAttackerDamage(attacker, rangeDiff);
        }
        
        public static int GetMinDamage(Traits attacker)
        {
            var rangeDiff = GetRangeDiff(attacker);

            return CalculateAttackerDamage(attacker, -rangeDiff);
        }
        
        public static float CalculateAttackRange(PlayerTraits attacker)
        {
            // TODO: Turn into ScriptableObject?
            var minRange = 4;
            var maxRange = 15;
            var maxDex = 125;

            var range = Mathf.Min(maxRange, minRange + (attacker.Dexterity / (maxRange * maxDex)));
            return range;
        }
    }
}