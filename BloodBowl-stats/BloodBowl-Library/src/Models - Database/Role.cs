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
            public List<Effect> effects { get; }


            public RoleData(string name, int price, int movement, int strength, int agility, int armor, List<Effect> effects)
            {
                this.name = name;
                this.price = price;
                this.movement = movement;
                this.strength = strength;
                this.agility = agility;
                this.armor = armor;
                this.effects = effects;
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
                    return new RoleData("Blitzer", 40000, 7, 3, 3, 8, new List<Effect> { Effect.SkillGeneralBlock });
                case Role.HumanCatcher:
                    return new RoleData("Catcher", 40000, 8, 2, 3, 8, new List<Effect> { Effect.SkillGeneralBlock });
                case Role.HumanLineMan:
                    return new RoleData("Line man", 40000, 6, 3, 3, 8, new List<Effect> { Effect.SkillGeneralBlock });
                case Role.HumanOgre:
                    return new RoleData("Ogre", 40000, 5, 5, 2, 9, new List<Effect> { Effect.SkillGeneralBlock });
                case Role.HumanThrower:
                    return new RoleData("Thrower", 40000, 6, 3, 3, 8, new List<Effect> { Effect.SkillGeneralBlock });

                default:
                    return new RoleData("Default", 200000, 10, 10, 10, 10, new List<Effect>());
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
        /// Name of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Name of the instance</returns>
        public static List<Effect> effects(this Role playerRole)
        {
            return playerRole.data().effects;
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
