using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

/* This script is used for creating playable characters or NPCs in a game. I originally used this script for a RPG, but it should be usable
for other genres. You can create child classes using this class as a base. */
namespace MMurray.GenericCode
{
    public abstract class Avatar : MonoBehaviour
    {
        public string avatarName;
        public string details;      //description of the avatar
        public float health;
        public float maxHealth;
        public float manaPoints;
        public float maxManaPoints;
        public float atp;           //attack power
        public float dfp;           //defense power
        public float spd;           //speed
        public float mag;           //magic power
        public float res;           //resistance against magic and ailments

        [Header("---Status Modifiers---")] //These would temporarily modify stats, like buff spells, etc. Usage example: (maxHealth = maxHealth * healthMod)
        public float healthMod = 1;
        public float manaMod = 1;
        public float atpMod = 1;
        public float dfpMod = 1;
        public float spdMod = 1;
        public float magMod = 1;
        public float resMod = 1;

        [Header("---Ailment Status---")]    //The values would normally range from 0 to 1, but should allow player to go past 1 in case the same resists
        public float resistPoison;          //come from multiple sources.
        public float resistParalysis;
        public float resistBlind;
        public float resistCharm;
        public float resistDeath;           //this would be used to protect against instant KO attacks

        [Header("---Elemental Resists---")] //Add as many or as little resists as needed
        public float fireResist;
        public float coldResist;
        public float lightningResist;

        public List<Skill> skills;                  //list of skills the avatar can choose from.
        public Dictionary<Skill, int> skillEffects; //this would be any buffs/debuffs/ailments the avatar is currently affected with. The int is the duration.

        public enum Status
        {
            /* DETAILS
            -------------
            Poisoned = Target loses health in an interval.
            Paralyzed (Stun) = Target cannot act
            Blind = Chance that an attack will miss.
            Charmed = target will attack random allies.
            Dead = Zero health.
            */
            Normal, Poisoned, Paralyzed, Blind, Charmed, Dead     
        }
        public Status status;

        public virtual void Attack(Avatar target) {}
        public virtual void UseSkill(Avatar target) {}
        public virtual void UseSkill(List<Avatar> targets) {}
        public virtual void Move() {}
    }


}