using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Traits", menuName = "Game Stats/Player Traits", order = 0)]
    public class PlayerTraits : Traits
    {
        public UnityAction GainedExperienceEvent;
        
        public int PointsLeft;

        [SerializeField] private int _experienceGained;

        public int ExperienceGained
        {
            get
            {
                return _experienceGained;
            }
            set
            {
                _experienceGained = value;
                GainedExperienceEvent?.Invoke();
            }
        }
    }
}