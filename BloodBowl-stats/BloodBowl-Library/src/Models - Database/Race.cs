using System;
using System.Collections.Generic;
using System.Linq;


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
        public class RaceData
        {
            public string name { get; }
            public List<Role> roles { get; }


            public RaceData(string name, List<Role> roles)
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
        private static RaceData data(this Race race)
        {
            switch (race)
            {
                // Humans and cie
                case Race.Humans:
                    return new RaceData("Humans", new List<Role> { Role.HumanBlitzer, Role.HumanCatcher, Role.HumanLineMan, Role.HumanOgre, Role.HumanThrower });

                case Race.Bretonnia:
                    return new RaceData("Bretonnia", new List<Role> { });

                case Race.Dwarfs:
                    return new RaceData("Dwarfs", new List<Role> { });

                case Race.Halfling:
                    return new RaceData("Halfling", new List<Role> { });


                // Evil - meh
                case Race.Orcs:
                    return new RaceData("Orcs", new List<Role> { });

                case Race.Goblins:
                    return new RaceData("Goblins", new List<Role> { });

                case Race.Ogre:
                    return new RaceData("Ogre", new List<Role> { });


                // Elves
                case Race.Elves:
                    return new RaceData("Elves", new List<Role> { });

                case Race.ElvesHigh:
                    return new RaceData("High Elves", new List<Role> { });

                case Race.ElvesDark:
                    return new RaceData("Dark Elves", new List<Role> { });

                case Race.ElvesWood:
                    return new RaceData("Wood Elves", new List<Role> { });

                // Weirdos
                case Race.LizardMen:
                    return new RaceData("Lizardmen", new List<Role> { });

                case Race.Norse:
                    return new RaceData("Norse", new List<Role> { });

                case Race.Amazon:
                    return new RaceData("Amazon", new List<Role> { });

                case Race.Khemri:
                    return new RaceData("Khemri", new List<Role> { });

                case Race.Necromentic:
                    return new RaceData("Necromentic", new List<Role> { });

                case Race.Vampire:
                    return new RaceData("Vampire", new List<Role> { });

                case Race.Undead:
                    return new RaceData("Undead", new List<Role> { });


                // Evil - really evil
                case Race.Skaven:
                    return new RaceData("Skaven", new List<Role> { });

                case Race.Chaos:
                    return new RaceData("Chaos", new List<Role> { });

                case Race.Nurgle:
                    return new RaceData("Nurgle", new List<Role> { });

                case Race.KhorneDeamons:
                    return new RaceData("Khorne Deamons", new List<Role> { });

                case Race.ChaosDwarfs:
                    return new RaceData("Chaos Dwarfs", new List<Role> { });

                case Race.Underworld:
                    return new RaceData("Underworld", new List<Role> { });

                default:
                    return new RaceData("UNKNOWN", new List<Role> { });
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



        /// <summary>
        /// Returns an array of all the Races
        /// </summary>
        /// <returns>An array of all the Races</returns>
        public static List<Race> GetAllRaces()
        {
            return Enum.GetValues(typeof(Race)).Cast<Race>().ToList();
        }
    }
}