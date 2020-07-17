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
        public static String name(this Race race)
        {
            switch (race)
            {
                // Humans and cie
                case Race.Humans:
                    return "Humans";

                case Race.Bretonnia:
                    return "Bretonnia";

                case Race.Dwarfs:
                    return "Dwarfs";

                case Race.Halfling:
                    return "Halfling";


                // Evil - meh
                case Race.Orcs:
                    return "Orcs";

                case Race.Goblins:
                    return "Goblins";

                case Race.Ogre:
                    return "Ogre";


                // Elves
                case Race.Elves:
                    return "Elves";

                case Race.ElvesHigh:
                    return "High Elves";

                case Race.ElvesDark:
                    return "Dark Elves";

                case Race.ElvesWood:
                    return "Wood Elves";

                // Weirdos
                case Race.LizardMen:
                    return "Lizardmen";

                case Race.Norse:
                    return "Norse";

                case Race.Amazon:
                    return "Amazon";

                case Race.Khemri:
                    return "Khemri";

                case Race.Necromentic:
                    return "Necromentic";

                case Race.Vampire:
                    return "Vampire";

                case Race.Undead:
                    return "Undead";


                // Evil - really evil
                case Race.Skaven:
                    return "Skaven";

                case Race.Chaos:
                    return "Chaos";

                case Race.Nurgle:
                    return "Nurgle";

                case Race.KhorneDeamons:
                    return "Khorne Deamons";

                case Race.ChaosDwarfs:
                    return "Chaos Dwarfs";

                case Race.Underworld:
                    return "Underworld";

                default:
                    return "UNKNOWN";
            }
        }
        public static List<Player> playerPrefabs(this Race race)
        {
            switch (race)
            {
                // Humans and cie
                case Race.Humans:
                    return new List<Player> {
                        new Player()
                    };
                /*
                case Race.Bretonnia:
                    return "Bretonnia";

                case Race.Dwarfs:
                    return "Dwarfs";

                case Race.Halfling:
                    return "Halfling";


                // Evil - meh
                case Race.Orcs:
                    return "Orcs";

                case Race.Goblins:
                    return "Goblins";

                case Race.Ogre:
                    return "Ogre";


                // Elves
                case Race.Elves:
                    return "Elves";

                case Race.ElvesHigh:
                    return "High Elves";

                case Race.ElvesDark:
                    return "Dark Elves";

                case Race.ElvesWood:
                    return "Wood Elves";

                // Weirdos
                case Race.LizardMen:
                    return "Lizardmen";

                case Race.Norse:
                    return "Norse";

                case Race.Amazon:
                    return "Amazon";

                case Race.Khemri:
                    return "Khemri";

                case Race.Necromentic:
                    return "Necromentic";

                case Race.Vampire:
                    return "Vampire";

                case Race.Undead:
                    return "Undead";


                // Evil - really evil
                case Race.Skaven:
                    return "Skaven";

                case Race.Chaos:
                    return "Chaos";

                case Race.Nurgle:
                    return "Nurgle";

                case Race.KhorneDeamons:
                    return "Khorne Deamons";

                case Race.ChaosDwarfs:
                    return "Chaos Dwarfs";

                case Race.Underworld:
                    return "Underworld";
                    */

                default:
                    return new List<Player>();
            }
        }
    }
}