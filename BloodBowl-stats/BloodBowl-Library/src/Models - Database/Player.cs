using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    [Serializable]
    public class Player
    {
        private Guid _id;
        private string _name;
        private int _xp;
        private List<Effect> _effects;
        private Team _team;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Player
        /// </summary>
        public Player()
        {
            id = Guid.Empty;
            name = String.Empty;
            xp = 0;
            effects = new List<Effect>();
            team = new Team();
        }


        /// <summary>
        /// Creates a new instance of a Player with according parameters
        /// </summary>
        /// <param name="name">Name of the Player</param>
        /// <param name="xp">Experience of the Player</param>
        /// <param name="effects">Effects of the Player</param>
        /// <param name="team">Team of the Player</param>
        public Player(string name, List<Effect> effects, Team team)
        {
            id = Guid.NewGuid();
            this.name = name;
            xp = 0;
            this.effects = effects;
            this.team = team;
        }


        /// <summary>
        /// Creates a complete instance of a Player with according parameters
        /// </summary>
        /// <param name="id">Id of the Player</param>
        /// <param name="name">Name of the Player</param>
        /// <param name="level">Level of the Player</param>
        /// <param name="effects">Effects of the Player</param>
        /// <param name="team">Team of the Player</param>
        public Player(Guid id, string name, int xp, List<Effect> effects, Team team)
        {
            this.id = id;
            this.name = name;
            this.xp = xp;
            this.effects = effects;
            this.team = team;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            return String.Format("\t\t\tPlayer : {0} - {1} lvl {2}, xp = {3}\n", id, name, level, xp);
        }



        // GETTER - SETTER
        public Guid id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = Util.CorrectString(value); }
        public int xp { get => _xp; set => _xp = value; }
        public List<Effect> effects { get => _effects; set => _effects = value; }
        [JsonIgnore]
        public Team team { get => _team; set => _team = value; }


        // PARAM
        [JsonIgnore]
        public bool isDead { get => effects.Contains(Effect.Dead); }
        [JsonIgnore]
        public int numberOfSkills { get => effects.Where(effect => effect.isSkill()).ToList().Count; }
        [JsonIgnore]
        public int numberOfWounds { get => effects.Where(effect => effect.isWound()).ToList().Count; }


        private List<int> levelThresholds = new List<int> { 6, 12, 36 };

        [JsonIgnore]
        public int level { get => numberOfSkills; }
        
        [JsonIgnore]
        public bool hasNewLevel
        {
            get
            {
                // We initialize a counter
                int cpt = 0;

                // Foreach time the XP outgrows a thresholds, we increment the counter
                foreach (int threshold in levelThresholds)
                {
                    if (threshold < xp)
                    {
                        cpt++;
                    }
                    else
                    {
                        break;
                    }
                }

                // If the Player has as many threshold outgrown as levels, he is fine
                // If not, he can have a new Level
                return cpt != level;
            }
        }

        [JsonIgnore]
        public int value
        {
            get
            {
                // We initialize a counter
                int baseCpt = 0;

                return baseCpt + effects.Where(effect => effect.isSkill()).ToList().Count * 20;
            }
        }
    }
}
