using System;
using System.Numerics;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.FantasyInventory.Scripts.Data;
using Platformer;
using ScriptableObjects;
using UI;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Character
{
    public abstract class CharacterWrapper : MonoBehaviour
    {
        public Traits Traits;
        public bool IsDead;

        protected Collider2D _collider;
        private IHealthUI _healthUIl;
        protected float _health;
        protected int _defense;
        
        [SerializeField] private Transform _damageSpawnPosition;
        [SerializeField] private GameObject _damagePrefab;
        
        protected IHealthUI _healthUI;

        [SerializeField] protected Assets.HeroEditor.Common.CharacterScripts.Character _character;

        [SerializeField] private Transform ArmL;
        [SerializeField] private Transform ArmR;

        protected AnimationEvents _animationEvents;
        
        protected virtual void Awake()
        {
            _defense = _defense;
            _collider = GetComponent<Collider2D>();
            _healthUI = GetComponent<IHealthUI>();
            _animationEvents = GetComponentInChildren<AnimationEvents>();
        }

        public virtual void ComeAlive()
        {
            IsDead = false;
            _healthUI?.SetHealth(1);
            _collider.enabled = true;
        }

        protected void DisplayDamage(float damage)
        {
            var position = _damageSpawnPosition.position;
            var damageText = Instantiate(_damagePrefab, position, Quaternion.identity).GetComponent<DamageText>();
            damageText.SetText(damage.ToString());
            damageText.SetPosition(position);
        }

        public Assets.HeroEditor.Common.CharacterScripts.Character GetCharacter()
        {
            return _character;
        }
        
        public void GetReady()
        {
            _character.GetReady();
            ArmL.Rotate(Vector3.forward, 47f);
            //RotateArm(ArmL, _character.BowRenderers[3].transform, ArmL.position + 1000 * Vector3.right, -40, 40);
        }

        private void Update()
        {
            //ArmL.transform.eulerAngles = new Vector3(0, 0, ArmL.transform.eulerAngles.z + 0.05f);
            
        }

        public abstract void ReceiveDamage(float damage);

        protected abstract void Die();
        
        /// <summary>
        /// Selected arm to position (world space) rotation, with limits.
        /// </summary>
        public void RotateArm(Transform arm, Transform weapon, Vector2 target, float angleMin, float angleMax) // TODO: Very hard to understand logic.
        {
            target = arm.transform.InverseTransformPoint(target);
            
            var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
            var angleToFirearm = Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
            var fix = weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude;

            if (fix < -1) fix = -1;
            if (fix > 1) fix = 1;

            var angleFix = Mathf.Asin(fix) * Mathf.Rad2Deg;
            var angle = angleToTarget + angleToFirearm + angleFix;

            angleMin += angleToFirearm;
            angleMax += angleToFirearm;

            var z = arm.transform.localEulerAngles.z;

            if (z > 180) z -= 360;

            if (z + angle > angleMax)
            {
                angle = angleMax;
            }
            else if (z + angle < angleMin)
            {
                angle = angleMin;
            }
            else
            {
                angle += z;
            }

            if (float.IsNaN(angle))
            {
                Debug.LogWarning(angle);
            }

            arm.transform.eulerAngles = new Vector3(0, 0, 47);
        }
    }
}