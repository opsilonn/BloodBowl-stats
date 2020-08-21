using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    [Serializable]
    public enum EffectType
    {
        // Skills
        SkillGeneral,
        SkillAgility,
        SkillPass,
        SkillStrength,
        SkillMutation,
        SkillExtraordinary,

        // Bonus
        BonusMovement,
        BonusStrength,
        BonusAgility,
        BonusArmor,

        // Casualty
        CasualtyBenign,
        CasualtyMovement,
        CasualtyStrength,
        CasualtyAgility,
        CasualtyArmor,
        CasualtyDeath
    }




    public static partial class EffectStuff
    {
        // IS AN EFFECT OF X-CATEGORY

        /// <summary>
        /// Returns whether an Effect is a Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Skill or not</returns>
        public static bool isSkill(this Effect effect)
        {
            return effect.type().ToString().StartsWith("Skill");
        }


        /// <summary>
        /// Returns whether an Effect is a General Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a General Skill or not</returns>
        public static bool isSkillGeneral(this Effect effect)
        {
            return effect.type() == EffectType.SkillGeneral;
        }


        /// <summary>
        /// Returns whether an Effect is an Agility Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is an Agility Skill or not</returns>
        public static bool isSkillAgility(this Effect effect)
        {
            return effect.type() == EffectType.SkillAgility;
        }


        /// <summary>
        /// Returns whether an Effect is a Pass Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Pass Skill or not</returns>
        public static bool isSkillPass(this Effect effect)
        {
            return effect.type() == EffectType.SkillPass;
        }


        /// <summary>
        /// Returns whether an Effect is a Strength Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Strength Skill or not</returns>
        public static bool isSkillStrength(this Effect effect)
        {
            return effect.type() == EffectType.SkillStrength;
        }


        /// <summary>
        /// Returns whether an Effect is a Mutation Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Mutation Skill or not</returns>
        public static bool isSkillMutation(this Effect effect)
        {
            return effect.type() == EffectType.SkillMutation;
        }


        /// <summary>
        /// Returns whether an Effect is an Extraordinary Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is an Extraordinary Skill or not</returns>
        public static bool isSkillExtraordinary(this Effect effect)
        {
            return effect.type() == EffectType.SkillExtraordinary;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty or not</returns>
        public static bool isCasualty(this Effect effect)
        {
            return effect.type().ToString().StartsWith("Casualty");
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing movement or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing movement or not</returns>
        public static bool isCasualtyMovement(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyMovement;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing strength or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing strength or not</returns>
        public static bool isCasualtyStrength(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyStrength;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing agility or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing agility or not</returns>
        public static bool isCasualtyAgility(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyAgility;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing armor or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing armor or not</returns>
        public static bool isCasualtyArmor(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyArmor;
        }






        /// <summary>
        /// Returns all the Effect type required for a Levelling up
        /// </summary>
        /// <param name="addMutation">Whether we add the mutation or not</param>
        /// <returns>All the Effect type required for a Levelling up</returns>
        public static List<EffectType> GetAllEffectTypesForLevelUp(bool addMutation)
        {
            /// We initiate a default list
            List<EffectType> list = new List<EffectType> {
                EffectType.SkillGeneral,
                EffectType.SkillAgility,
                EffectType.SkillPass,
                EffectType.SkillStrength,
            };

            // If needed, we add the mutation
            if (addMutation)
            {
                list.Add(EffectType.SkillMutation);
            }

            // We return the list
            return list;
        }


        /// <summary>
        /// Returns all the Effect type of a given EffectType
        /// </summary>
        /// <param name="type">EffectType of which we are searching the Effects</param>
        /// <returns>All the Effect type of a given EffectType</returns>
        public static List<Effect> GetAllSkillsFromType(EffectType type)
        {
            return GetAllSkills().Where(skill => skill.type() == type).ToList();
        }






        // GET ALL EFFECT OF X-CATEGORY


        /// <summary>
        /// Returns an array of all the Effects
        /// </summary>
        /// <returns>An array of all the Effects</returns>
        public static List<Effect> GetAllEffects()
        {
            return Enum.GetValues(typeof(Effect)).Cast<Effect>().ToList();
        }


        /// <summary>
        /// Returns an array of all the Skill Effects
        /// </summary>
        /// <returns>An array of all the Skill Effects</returns>
        public static List<Effect> GetAllSkills()
        {
            return GetAllEffects().Where(effect => effect.isSkill()).ToList();
        }


        /// <summary>
        /// Returns an array of all the General Skill Effects
        /// </summary>
        /// <returns>An array of all the General Skill Effects</returns>
        public static List<Effect> GetAllSkillsGeneral()
        {
            return GetAllEffects().Where(effect => effect.isSkillGeneral()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Agility Skill Effects
        /// </summary>
        /// <returns>An array of all the Agility Skill Effects</returns>
        public static List<Effect> GetAllSkillsAgility()
        {
            return GetAllEffects().Where(effect => effect.isSkillAgility()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Pass Skill Effects
        /// </summary>
        /// <returns>An array of all the Pass Skill Effects</returns>
        public static List<Effect> GetAllSkillsPass()
        {
            return GetAllEffects().Where(effect => effect.isSkillPass()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Strength Skill Effects
        /// </summary>
        /// <returns>An array of all the Strength Skill Effects</returns>
        public static List<Effect> GetAllSkillsStrength()
        {
            return GetAllEffects().Where(effect => effect.isSkillStrength()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Mutation Skill Effects
        /// </summary>
        /// <returns>An array of all the Mutation Skill Effects</returns>
        public static List<Effect> GetAllSkillsMutation()
        {
            return GetAllEffects().Where(effect => effect.isSkillMutation()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Extraordinary Skill Effects
        /// </summary>
        /// <returns>An array of all the Extraordinary Skill Effects</returns>
        public static List<Effect> GetAllSkillsExtraordinary()
        {
            return GetAllEffects().Where(effect => effect.isSkillExtraordinary()).ToList();
        }
    }
}
