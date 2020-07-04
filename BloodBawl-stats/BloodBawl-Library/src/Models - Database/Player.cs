using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBawl_Library
{
    [Serializable]
    public class Player
    {
        private Guid _id;
        private string _name;
        private int _level;
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
            level = 1;
            effects = new List<Effect>();
            team = new Team();
        }


        /// <summary>
        /// Creates a new instance of a Player with according parameters
        /// </summary>
        /// <param name="name">Name of the Player</param>
        /// <param name="level">Level of the Player</param>
        /// <param name="effects">Effects of the Player</param>
        /// <param name="team">Team of the Player</param>
        public Player(string name, int level, List<Effect> effects, Team team)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.level = level;
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
        public Player(Guid id, string name, int level, List<Effect> effects, Team team)
        {
            this.id = id;
            this.name = name;
            this.level = level;
            this.effects = effects;
            this.team = team;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            return String.Format("\t\tPlayer : {0} - {1}: level {2}\n", id, name, level);
        }



        // GETTER - SETTER
        public Guid id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = Util.CorrectString(value); }
        public int level { get => _level; set => _level = value; }
        public List<Effect> effects { get => _effects; set => _effects = value; }
        public Team team { get => _team; set => _team = value; }


        // PARAM
        public bool isDead { get => effects.Contains(Effect.Mort); }
    }
}
