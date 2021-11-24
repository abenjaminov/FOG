using System;

namespace Persistence.PersistenceObjects
{
    [Serializable]
    public class PlayerTraitsPersistence
    {
        internal string Name;
        internal bool IsNameSet;
        internal float CurrentHealth;
        internal int Strength;
        internal int Dexterity;
        internal int Intelligence;
        internal int Constitution;
        internal int PointsLeft;
        internal int Level;
        internal int ResistancePointsGained;
        internal float MonsterStateResistance;
    }
}