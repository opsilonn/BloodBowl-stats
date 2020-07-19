using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    public enum Race
    {
        // Humans and cie
        Humans,
        Bretonnia,
        Dwarfs,
        Halfling,

        // Evil - meh
        Orcs,
        Goblins,
        Ogre,

        // Elves
        Elves,
        ElvesHigh,
        ElvesDark,
        ElvesWood,

        // Weirdos
        LizardMen,
        Norse,
        Amazon,
        Khemri,
        Necromentic,
        Vampire,
        Undead,

        // Evil - really evil
        Skaven,
        Chaos,
        Nurgle,
        KhorneDeamons,
        ChaosDwarfs,
        Underworld
    }


    public static class RaceStuff
    {
        public class PlayerRoleData
        {
            public string name { get; }
            public List<Role> roles { get; }


            public PlayerRoleData(string name, List<Role> roles)
            {
                this.name = name;
                this.roles = roles;
            }
        }


        /// <summary>
        /// Data of the instance
        /// </summary>
        /// <param name="race">Race we are analysing</param>
        /// <returns>Data of the instance</returns>
        private static PlayerRoleData data(this Race race)
        {
            switch (race)
            {
                // Humans and cie
                case Race.Humans:
                    return new PlayerRoleData("Humans", new List<Role> { Role.HumanBlitzer, Role.HumanCatcher, Role.HumanLineMan, Role.HumanOgre, Role.HumanThrower });

                case Race.Bretonnia:
                    return new PlayerRoleData("Bretonnia", new List<Role> { });

                case Race.Dwarfs:
                    return new PlayerRoleData("Dwarfs", new List<Role> { });

                case Race.Halfling:
                    return new PlayerRoleData("Halfling", new List<Role> { });


                // Evil - meh
                case Race.Orcs:
                    return new PlayerRoleData("Orcs", new List<Role> { });

                case Race.Goblins:
                    return new PlayerRoleData("Goblins", new List<Role> { });

                case Race.Ogre:
                    return new PlayerRoleData("Ogre", new List<Role> { });


                // Elves
                case Race.Elves:
                    return new PlayerRoleData("Elves", new List<Role> { });

                case Race.ElvesHigh:
                    return new PlayerRoleData("High Elves", new List<Role> { });

                case Race.ElvesDark:
                    return new PlayerRoleData("Dark Elves", new List<Role> { });

                case Race.ElvesWood:
                    return new PlayerRoleData("Wood Elves", new List<Role> { });

                // Weirdos
                case Race.LizardMen:
                    return new PlayerRoleData("Lizardmen", new List<Role> { });

                case Race.Norse:
                    return new PlayerRoleData("Norse", new List<Role> { });

                case Race.Amazon:
                    return new PlayerRoleData("Amazon", new List<Role> { });

                case Race.Khemri:
                    return new PlayerRoleData("Khemri", new List<Role> { });

                case Race.Necromentic:
                    return new PlayerRoleData("Necromentic", new List<Role> { });

                case Race.Vampire:
                    return new PlayerRoleData("Vampire", new List<Role> { });

                case Race.Undead:
                    return new PlayerRoleData("Undead", new List<Role> { });


                // Evil - really evil
                case Race.Skaven:
                    return new PlayerRoleData("Skaven", new List<Role> { });

                case Race.Chaos:
                    return new PlayerRoleData("Chaos", new List<Role> { });

                case Race.Nurgle:
                    return new PlayerRoleData("Nurgle", new List<Role> { });

                case Race.KhorneDeamons:
                    return new PlayerRoleData("Khorne Deamons", new List<Role> { });

                case Race.ChaosDwarfs:
                    return new PlayerRoleData("Chaos Dwarfs", new List<Role> { });

                case Race.Underworld:
                    return new PlayerRoleData("Underworld", new List<Role> { });

                default:
                    return new PlayerRoleData("UNKNOWN", new List<Role> { });
            }
        }


        /// <summary>
        /// Returns the name of a given Race
        /// </summary>
        /// <param name="race">Race of which we want to know the name</param>
        /// <returns>The name of a given Race</returns>
        public static String name(this Race race)
        {
            return race.data().name;
        }

        /// <summary>
        /// Returns the list of Roles of a given Race
        /// </summary>
        /// <param name="race">Race of which we want to know the Roles</param>
        /// <returns>The Roles of a given Race</returns>
        public static List<Role> playerRoles(this Race race)
        {
            return race.data().roles;
        }
    }
}