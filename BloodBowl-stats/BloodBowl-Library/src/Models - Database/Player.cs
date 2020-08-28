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
        private Role _role;
        private int _xp;
        private List<Perk> _perks;
        private Team _team;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Player
        /// </summary>
        public Player()
        {
            id = Guid.Empty;
            name = String.Empty;
            role = Role.Default;
            xp = 0;
            perks = new List<Perk>();
            team = new Team();
        }


        /// <summary>
        /// Creates a new instance of a Player with according parameters
        /// </summary>
        /// <param name="name">Name of the Player</param>
        /// <param name="role">Role of the Player</param>
        /// <param name="team">Team of the Player</param>
        public Player(string name, Role role, Team team)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.role = role;
            xp = 0;
            perks = new List<Perk>();
            this.team = team;
        }


        /// <summary>
        /// Creates a complete instance of a Player with according parameters
        /// </summary>
        /// <param name="id">Id of the Player</param>
        /// <param name="name">Name of the Player</param>
        /// <param name="role">Role of the Player</param>
        /// <param name="xp">XP of the Player</param>
        /// <param name="perks">Perks of the Player</param>
        /// <param name="team">Team of the Player</param>
        public Player(Guid id, string name, Role role, int xp, List<Perk> perks, Team team)
        {
            this.id = id;
            this.name = name;
            this.role = role;
            this.xp = xp;
            this.perks = perks;
            this.team = team;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            return String.Format("Player : {0} - {1} lvl {2}, xp = {3}\n", id, name, level, xp);
        }



        // GETTER - SETTER
        public Guid id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = Util.CorrectString(value); }
        public Role role { get => _role; set => _role = value; }
        public int xp { get => _xp; set => _xp = value; }
        public List<Perk> perks { get => _perks; set => _perks = value; }
        [JsonIgnore]
        public List<Perk> perksAll
        {
            get
            {
                // We initialize a list
                List<Perk> toReturn = new List<Perk>();

                // We add : 1) the role's effects 2) the Players acquired perks
                role.perks().ForEach(perk => toReturn.Add(perk));
                _perks.ForEach(perk => toReturn.Add(perk));

                // We return the list
                return toReturn;
            }
        }
        [JsonIgnore]
        public Team team { get => _team; set => _team = value; }
        [JsonIgnore]
        public int movement
        { get =>
                role.movement()
                - perks.Where(perk => perk.isCasualtyMovement()).ToList().Count
                + perks.Where(perk => perk == Perk.BonusMovement).ToList().Count;
        }
        [JsonIgnore]
        public int strength
        {
            get =>
                    role.strength()
                    - perks.Where(perk => perk.isCasualtyStrength()).ToList().Count
                    + perks.Where(perk => perk == Perk.BonusStrength).ToList().Count;
        }
        [JsonIgnore]
        public int agility
        {
            get =>
                    role.agility()
                    - perks.Where(perk => perk.isCasualtyAgility()).ToList().Count
                    + perks.Where(perk => perk == Perk.BonusAgility).ToList().Count;
        }
        [JsonIgnore]
        public int armor
        {
            get =>
                    role.armor()
                    - perks.Where(perk => perk.isCasualtyArmor()).ToList().Count
                    + perks.Where(perk => perk == Perk.BonusArmor).ToList().Count;
        }


        // PARAM
        [JsonIgnore]
        public bool IsComplete
        {
            get => (
            id != Guid.Empty
            && name != String.Empty
            );
        }
        [JsonIgnore]
        public bool isDead { get => perks.Contains(Perk.Dead); }
        [JsonIgnore]
        public List<Perk> skills { get => perks.Where(perk => perk.isSkill()).ToList(); }
        [JsonIgnore]
        public List<Perk> casualties { get => perks.Where(perk => perk.isCasualty()).ToList(); }
        [JsonIgnore]
        public int numberOfSkills { get => skills.Count; }
        [JsonIgnore]
        public int numberOfCasualty { get => casualties.Count; }


        private List<int> levelThresholds = new List<int> { 6, 12, 36 };

        [JsonIgnore]
        public int level { get => 1 + numberOfSkills; }


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
                int baseCpt = role.price();

                return baseCpt + perks.Where(perk => perk.isSkill()).ToList().Count * 20;
            }
        }
    }
}
