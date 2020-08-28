using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    public enum Role
    {
        Default,

        // Human
        HumanBlitzer,
        HumanCatcher,
        HumanLineMan,
        HumanOgre,
        HumanThrower
    }




    public static class RoleStuff
    {
        public class RoleData
        {
            public string name { get; }
            public int price { get; }
            public int movement { get; }
            public int strength { get; }
            public int agility { get; }
            public int armor { get; }
            public List<Perk> perks { get; }
            public List<PerkType> perkTypes { get; }


            public RoleData(string name, int price, int movement, int strength, int agility, int armor, List<Perk> perk, List<PerkType> perkTypes)
            {
                this.name = name;
                this.price = price;
                this.movement = movement;
                this.strength = strength;
                this.agility = agility;
                this.armor = armor;
                this.perks = perk;
                this.perkTypes = perkTypes;
            }
        }


        /// <summary>
        /// Data of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Data of the instance</returns>
        private static RoleData data(this Role playerRole)
        {
            switch (playerRole)
            {
                // Humans
                case Role.HumanBlitzer:
                    return new RoleData("Blitzer", 90000, 7, 3, 3, 8, new List<Perk> { Perk.Block }, new List<PerkType> { PerkType.SkillGeneral, PerkType.SkillStrength });
                case Role.HumanCatcher:
                    return new RoleData("Catcher", 70000, 8, 2, 3, 8, new List<Perk> { Perk.Dodge, Perk.Catch }, new List<PerkType> { PerkType.SkillGeneral, PerkType.SkillAgility });
                case Role.HumanLineMan:
                    return new RoleData("Line man", 50000, 6, 3, 3, 8, new List<Perk>(), new List<PerkType> { PerkType.SkillGeneral });
                case Role.HumanOgre:
                    return new RoleData("Ogre", 130000, 5, 5, 2, 9, new List<Perk> { Perk.BoneHead, Perk.MightyBlow, Perk.ThickSkull, Perk.ThrowTeamMate, Perk.Loner }, new List<PerkType> { PerkType.SkillStrength });
                case Role.HumanThrower:
                    return new RoleData("Thrower", 70000, 6, 3, 3, 8, new List<Perk> { Perk.Pass, Perk.SureHands }, new List<PerkType> { PerkType.SkillGeneral, PerkType.SkillPass });

                default:
                    return new RoleData("Default", 200000, 10, 10, 10, 10, new List<Perk>(), new List<PerkType>());
            }
        }


        /// <summary>
        /// Name of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Name of the instance</returns>
        public static String name(this Role playerRole)
        {
            return playerRole.data().name;
        }


        /// <summary>
        /// Price of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Price of the instance</returns>
        public static int price(this Role playerRole)
        {
            return playerRole.data().price;
        }


        /// <summary>
        /// Movement of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Movement of the instance</returns>
        public static int movement(this Role playerRole)
        {
            return playerRole.data().movement;
        }


        /// <summary>
        /// Force of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Force of the instance</returns>
        public static int strength(this Role playerRole)
        {
            return playerRole.data().strength;
        }


        /// <summary>
        /// Agility of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Agility of the instance</returns>
        public static int agility(this Role playerRole)
        {
            return playerRole.data().agility;
        }


        /// <summary>
        /// Armor of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Armor of the instance</returns>
        public static int armor(this Role playerRole)
        {
            return playerRole.data().armor;
        }


        /// <summary>
        /// Perks of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Effects of the instance</returns>
        public static List<Perk> perks(this Role playerRole)
        {
            return playerRole.data().perks;
        }


        /// <summary>
        /// Types of Perks the instance can level up with
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Types of effects the instance can level up with</returns>
        public static List<PerkType> perkTypes(this Role playerRole)
        {
            return playerRole.data().perkTypes;
        }





        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public static string ToStringCustom(this Role playerRole)
        {
            return String.Format("{0} - {1}\nM: {2} S: {3} Ag: {4} Ar:{5}\n",
                playerRole.name(),
                playerRole.price(),
                playerRole.movement(),
                playerRole.strength(),
                playerRole.agility(),
                playerRole.armor());
        }








        /// <summary>
        /// Returns an array of all the Roles
        /// </summary>
        /// <returns>An array of all the Roles</returns>
        public static List<Role> GetAllRoles()
        {
            return Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
        }
    }
}
