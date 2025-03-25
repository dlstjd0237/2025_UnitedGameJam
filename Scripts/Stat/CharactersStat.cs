using BIS.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BIS.Stats
{
    public class CharactersStat : ScriptableObject
    {
        public Stat maxHealth;
        public Stat moveSpeed;
        public Stat jumpCount;
        public Stat jumpPower;
        public Stat attackDamage;

        protected Entity _owner;

        protected Dictionary<StatType, Stat> _statDictionary;


        public virtual void SetOwner(Entity owner)
        {
            _owner = owner;
        }

        public void AddStatPoint(StatType stat, int point)
        {
            _statDictionary[stat].AddModifier(point);
        }
        public virtual void IncreaseStatBy(int modifyValue, float duration, StatType type)
        {
            _owner.StartCoroutine(StatModifyCoroutine(modifyValue, duration, type));
        }

        private IEnumerator StatModifyCoroutine(int modifyValue, float duration, StatType type)
        {
            _statDictionary[type].AddModifier(modifyValue);
            yield return new WaitForSeconds(duration);
            _statDictionary[type].RemoveModifier(modifyValue);
        }

        protected virtual void OnEnable()
        {
            _statDictionary = new Dictionary<StatType, Stat>();
        }


        public int GetMaxHealth()
        {
            return maxHealth.GetValue();
        }

    }

}
