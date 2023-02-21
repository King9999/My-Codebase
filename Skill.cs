using UnityEngine;
using System.Collections.Generic;

/* base class for all effects in the game, including spells, consumable item effects. If you wanted to give an item an effect, you would give it 
one of these. The item would have space to add a Skill scriptable object (the item itself would also be a scriptable object). */
namespace MMurray.GenericCode
{
    public abstract class Skill : ScriptableObject
    {
        public string skillName;
        public Sprite skillIcon;
        public string description;
        public int manaCost;
        public float power;             //potency of a skill. It is added to either a player's MAG or ATP stat.
        public bool isPassive;          //if true, skill is always active and has no mana cost.
        protected float totalDamage;
        protected bool skillActivated;  //applies mainly to skills that have have a duration
        public bool hasDuration;
        public int turnDuration;        //only counts if a skill has a duration.
        protected int durationLeft {get; set;}

        //enums
        public enum Target
        {
            None, Self, OneEnemy, OneHero, AllEnemies, AllHeroes
        }

        public Target targetType;

        public virtual void Activate(Avatar target, Color borderColor) 
        {
            skillActivated = true;
            durationLeft = hasDuration == true ? turnDuration : 0;
        }

        //NOTE: Cannot use List<Avatar>. Must use ienumerable<Avatar> instead of List<Avatar> due to covariance 
        public virtual void Activate(List<Avatar> target) 
        {
            skillActivated = true;
            durationLeft = hasDuration == true ? turnDuration : 0;
        }

        public virtual void Activate()
        {
            skillActivated = true;
            durationLeft = hasDuration == true ? turnDuration : 0;
        }


        public bool SkillActivated() {return skillActivated;}
        public void SetActiveStatus(bool state)
        {
            skillActivated = state;
        }

        public void ReduceDuration(Dictionary<Skill, int> skillEffects, Skill skill)
        {
            if (!hasDuration) return;

            /*durationLeft--;
            if (durationLeft <= 0)
            {
                skillActivated = false;
            }*/

            skillEffects[skill]--;
            Debug.Log(skill.skillName + " duration: " + skillEffects[skill]);
            if (skillEffects[skill] <= 0)
                skillActivated = false;
        }

        public void ReduceMp(Avatar user)
        {
            user.manaPoints -= manaCost;
        }

        //only applies to skills with a duration
        public virtual void RemoveEffects(Avatar target) {}
        public bool EffectExpired(Dictionary<Skill, int> skillEffects, Skill skill)
        {
            //return durationLeft <= 0;
            return skillEffects[skill] <= 0;     //returns skill duration.
        }
        
    }
}
