using System;
using System.Collections.Generic;

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
            public int movement { get; }
            public int strength { get; }
            public int agility { get; }
            public int armor { get; }
            public List<Effect> effects { get; }


            public RoleData(string name, int movement, int strength, int agility, int armor, List<Effect> effects)
            {
                this.name = name;
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
                    return new RoleData("Blitzer", 3, 3, 3, 3, new List<Effect> { Effect.SkillBlock });
                case Role.HumanCatcher:
                    return new RoleData("Catcher", 3, 3, 3, 3, new List<Effect> { Effect.SkillBlock });
                case Role.HumanLineMan:
                    return new RoleData("Line man", 3, 3, 3, 3, new List<Effect> { Effect.SkillBlock });
                case Role.HumanOgre:
                    return new RoleData("Ogre", 3, 3, 3, 3, new List<Effect> { Effect.SkillBlock });
                case Role.HumanThrower:
                    return new RoleData("Thrower", 3, 3, 3, 3, new List<Effect> { Effect.SkillBlock });

                default:
                    return new RoleData("Default", 10, 10, 10, 10, new List<Effect>());
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
        /// Name of the instance
        /// </summary>
        /// <param name="playerRole">Role we are analysing</param>
        /// <returns>Name of the instance</returns>
        public static List<Effect> effects(this Role playerRole)
        {
            return playerRole.data().effects;
        }
    }
}
