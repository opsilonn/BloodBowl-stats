using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    [Serializable]
    public enum PerkType
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
        CasualtyRecovery,
        CasualtyNiggling,
        CasualtyMovement,
        CasualtyStrength,
        CasualtyAgility,
        CasualtyArmor,
        CasualtyDeath
    }




    public static partial class PerkStuff
    {
        // IS AN Perk OF X-CATEGORY

        /// <summary>
        /// Returns whether an Perk is a Skill or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Skill or not</returns>
        public static bool isSkill(this Perk perk)
        {
            return perk.type().ToString().StartsWith("Skill");
        }


        /// <summary>
        /// Returns whether an Perk is a General Skill or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a General Skill or not</returns>
        public static bool isSkillGeneral(this Perk perk)
        {
            return perk.type() == PerkType.SkillGeneral;
        }


        /// <summary>
        /// Returns whether an Perk is an Agility Skill or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is an Agility Skill or not</returns>
        public static bool isSkillAgility(this Perk perk)
        {
            return perk.type() == PerkType.SkillAgility;
        }


        /// <summary>
        /// Returns whether an Perk is a Pass Skill or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Pass Skill or not</returns>
        public static bool isSkillPass(this Perk perk)
        {
            return perk.type() == PerkType.SkillPass;
        }


        /// <summary>
        /// Returns whether an Perk is a Strength Skill or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Strength Skill or not</returns>
        public static bool isSkillStrength(this Perk perk)
        {
            return perk.type() == PerkType.SkillStrength;
        }


        /// <summary>
        /// Returns whether an Perk is a Mutation Skill or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Mutation Skill or not</returns>
        public static bool isSkillMutation(this Perk perk)
        {
            return perk.type() == PerkType.SkillMutation;
        }


        /// <summary>
        /// Returns whether an Perk is an Extraordinary Skill or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is an Extraordinary Skill or not</returns>
        public static bool isSkillExtraordinary(this Perk perk)
        {
            return perk.type() == PerkType.SkillExtraordinary;
        }


        /// <summary>
        /// Returns whether an Perk is a Casualty or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Casualty or not</returns>
        public static bool isCasualty(this Perk perk)
        {
            return perk.type().ToString().StartsWith("Casualty");
        }


        /// <summary>
        /// Returns whether an Perk is a Benign Casualty
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Benign Casualty</returns>
        public static bool isCasualtyBenign(this Perk perk)
        {
            return perk.type() == PerkType.CasualtyBenign;
        }


        /// <summary>
        /// Returns whether an Perk is a Recovery Casualty
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Recovery Casualty</returns>
        public static bool isCasualtyRecovery(this Perk perk)
        {
            return perk.type() == PerkType.CasualtyRecovery;
        }


        /// <summary>
        /// Returns whether an Perk is a Niggling Casualty
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Niggling Casualty</returns>
        public static bool isCasualtyNiggling(this Perk perk)
        {
            return perk.type() == PerkType.CasualtyNiggling;
        }


        /// <summary>
        /// Returns whether an Perk is a Casualty decreasing movement or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Casualty decreasing movement or not</returns>
        public static bool isCasualtyMovement(this Perk perk)
        {
            return perk.type() == PerkType.CasualtyMovement;
        }


        /// <summary>
        /// Returns whether an Perk is a Casualty decreasing strength or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Casualty decreasing strength or not</returns>
        public static bool isCasualtyStrength(this Perk perk)
        {
            return perk.type() == PerkType.CasualtyStrength;
        }


        /// <summary>
        /// Returns whether an Perk is a Casualty decreasing agility or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Casualty decreasing agility or not</returns>
        public static bool isCasualtyAgility(this Perk perk)
        {
            return perk.type() == PerkType.CasualtyAgility;
        }


        /// <summary>
        /// Returns whether an Perk is a Casualty decreasing armor or not
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Whether an Perk is a Casualty decreasing armor or not</returns>
        public static bool isCasualtyArmor(this Perk perk)
        {
            return perk.type() == PerkType.CasualtyArmor;
        }





        /// <summary>
        /// Gets all PerkTypes a Player's Role can level in, according to 2 dices
        /// </summary>
        /// <param name="role">Role of the Player that levels up</param>
        /// <param name="dice1">First dice</param>
        /// <param name="dice2">Second dice</param>
        /// <returns>All the PerkTypes a Player's Role can level in, according to 2 dices</returns>
        public static List<PerkType> GetPerkTypesForLevelUp(Role role, int dice1, int dice2)
        {
            // We initialize our list
            List<PerkType> types = new List<PerkType>();
            int diceTotal = dice1 + dice2;

            // We first add any Stat Bonus
            if (diceTotal == 10)
            {
                types.Add(PerkType.BonusMovement);
                types.Add(PerkType.BonusArmor);
            }
            else if (diceTotal == 11)
            {
                types.Add(PerkType.BonusAgility);
            }
            else if (diceTotal == 12)
            {
                types.Add(PerkType.BonusStrength);
            }

            // Then, we select the Perk Types the player can level up in
            if (dice1 == dice2)
            {
                // Add all generic types
                types.Add(PerkType.SkillGeneral);
                types.Add(PerkType.SkillAgility);
                types.Add(PerkType.SkillPass);
                types.Add(PerkType.SkillStrength);

                // If needed, we add the mutation
                if (role.perkTypes().Contains(PerkType.SkillMutation))
                {
                    types.Add(PerkType.SkillMutation);
                }
            }
            else
            {
                // Add only the default types
                types.AddRange(role.perkTypes());
            }

            // We return the types
            return types;
        }


        /// <summary>
        /// Returns all the Perk type accessible for a Levelling up
        /// </summary>
        /// <param name="types">PerkTypes the Player can level up in</param>
        /// <returns>All the Perk type accessible for a Levelling up</returns>
        public static List<List<Perk>> GetPerksForLevelUp(List<PerkType> types)
        {
            List<List<Perk>> Perks = new List<List<Perk>>();

            types.ForEach(type => Perks.Add(PerkStuff.GetAllPerksFromType(type)));

            return Perks;
        }






        // GET ALL Perk OF X-CATEGORY


        /// <summary>
        /// Returns an array of all the Perks
        /// </summary>
        /// <returns>An array of all the Perks</returns>
        public static List<Perk> GetAllPerks()
        {
            return Enum.GetValues(typeof(Perk)).Cast<Perk>().ToList();
        }
        

        /// <summary>
        /// Returns all the Perk type of a given PerkType
        /// </summary>
        /// <param name="type">PerkType of which we are searching the Perks</param>
        /// <returns>All the Perk type of a given PerkType</returns>
        public static List<Perk> GetAllPerksFromType(PerkType type)
        {
            return GetAllPerks().Where(skill => skill.type() == type).ToList();
        }


        /// <summary>
        /// Returns an array of all the Skill Perks
        /// </summary>
        /// <returns>An array of all the Skill Perks</returns>
        public static List<Perk> GetAllSkills()
        {
            return GetAllPerks().Where(Perk => Perk.isSkill()).ToList();
        }


        /// <summary>
        /// Returns an array of all the General Skill Perks
        /// </summary>
        /// <returns>An array of all the General Skill Perks</returns>
        public static List<Perk> GetAllSkillsGeneral()
        {
            return GetAllPerks().Where(Perk => Perk.isSkillGeneral()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Agility Skill Perks
        /// </summary>
        /// <returns>An array of all the Agility Skill Perks</returns>
        public static List<Perk> GetAllSkillsAgility()
        {
            return GetAllPerks().Where(Perk => Perk.isSkillAgility()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Pass Skill Perks
        /// </summary>
        /// <returns>An array of all the Pass Skill Perks</returns>
        public static List<Perk> GetAllSkillsPass()
        {
            return GetAllPerks().Where(Perk => Perk.isSkillPass()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Strength Skill Perks
        /// </summary>
        /// <returns>An array of all the Strength Skill Perks</returns>
        public static List<Perk> GetAllSkillsStrength()
        {
            return GetAllPerks().Where(Perk => Perk.isSkillStrength()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Mutation Skill Perks
        /// </summary>
        /// <returns>An array of all the Mutation Skill Perks</returns>
        public static List<Perk> GetAllSkillsMutation()
        {
            return GetAllPerks().Where(Perk => Perk.isSkillMutation()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Extraordinary Skill Perks
        /// </summary>
        /// <returns>An array of all the Extraordinary Skill Perks</returns>
        public static List<Perk> GetAllSkillsExtraordinary()
        {
            return GetAllPerks().Where(Perk => Perk.isSkillExtraordinary()).ToList();
        }
    }
}
