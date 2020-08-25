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
        /// Gets all EffectTypes a Player's Role can level in, according to 2 dices
        /// </summary>
        /// <param name="role">Role of the Player that levels up</param>
        /// <param name="dice1">First dice</param>
        /// <param name="dice2">Second dice</param>
        /// <returns>All the EffectTypes a Player's Role can level in, according to 2 dices</returns>
        public static List<EffectType> GetEffectTypesForLevelUp(Role role, int dice1, int dice2)
        {
            // We initialize our list
            List<EffectType> types = new List<EffectType>();
            int diceTotal = dice1 + dice2;

            // We first add any Stat Bonus
            if (diceTotal == 10)
            {
                types.Add(EffectType.BonusMovement);
                types.Add(EffectType.BonusArmor);
            }
            else if (diceTotal == 11)
            {
                types.Add(EffectType.BonusAgility);
            }
            else if (diceTotal == 12)
            {
                types.Add(EffectType.BonusStrength);
            }

            // Then, we select the Effect Types the player can level up in
            if (dice1 == dice2)
            {
                // Add all generic types
                types.Add(EffectType.SkillGeneral);
                types.Add(EffectType.SkillAgility);
                types.Add(EffectType.SkillPass);
                types.Add(EffectType.SkillStrength);

                // If needed, we add the mutation
                if (role.effectTypes().Contains(EffectType.SkillMutation))
                {
                    types.Add(EffectType.SkillMutation);
                }
            }
            else
            {
                // Add only the default types
                types.AddRange(role.effectTypes());
            }

            // We return the types
            return types;
        }


        /// <summary>
        /// Returns all the Effect type accessible for a Levelling up
        /// </summary>
        /// <param name="types">EffectTypes the Player can level up in</param>
        /// <returns>All the Effect type accessible for a Levelling up</returns>
        public static List<List<Effect>> GetEffectsForLevelUp(List<EffectType> types)
        {
            List<List<Effect>> effects = new List<List<Effect>>();

            types.ForEach(type => effects.Add(EffectStuff.GetAllEffectsFromType(type)));

            return effects;
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
        /// Returns all the Effect type of a given EffectType
        /// </summary>
        /// <param name="type">EffectType of which we are searching the Effects</param>
        /// <returns>All the Effect type of a given EffectType</returns>
        public static List<Effect> GetAllEffectsFromType(EffectType type)
        {
            return GetAllEffects().Where(skill => skill.type() == type).ToList();
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
