using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBawl_Library
{
    [Serializable]
    public class Team
    {
        private Guid _id;
        private string _name;
        private List<Guid> _idPlayers;
        private Guid _idCoach;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Team
        /// </summary>
        public Team()
        {
            id = Guid.Empty;
            name = String.Empty;
            // players = new List<Player>();
            idCoach = Guid.Empty;
        }


        /// <summary>
        /// Creates a new instance of a Team with according parameters
        /// </summary>
        /// <param name="name">Name of the Team</param>
        /// <param name="name">Players of the Team</param>
        /// <param name="idCoach">Id of the Coach of the Team</param>
        public Team(string name, List<Player> players, Guid idCoach)
        {
            id = Guid.NewGuid();
            this.name = name;
            // this.players = players;
            this.idCoach = idCoach;
        }


        /// <summary>
        /// Creates a complete instance of a Team with according parameters
        /// </summary>
        /// <param name="id">Id of the Team</param>
        /// <param name="name">Name of the Team</param>
        /// <param name="name">Players of the Team</param>
        /// <param name="idCoach">Id of the Coach of the Team</param>
        public Team(Guid id, string name, List<Player> players, Guid idCoach)
        {
            this.id = id;
            this.name = name;
            // this.players = players;
            this.idCoach = idCoach;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            /*
            string s = String.Format("\tTeam : {0} - {1} : {2} players\n", id, name, players.Count);

            foreach (Player player in players)
            {
                s += player.ToString();
            }
            */
            string s = String.Format("\tTeam : {0} - {1} : ?? players\n", id, name);

            return s;
        }



        // SERIALIZATION

        /// <summary>
        /// Return a JSON string representing the instance
        /// </summary>
        /// <returns>A JSON string representing the instance</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Returns a Team instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A Team instance from a JSON string</returns>
        public static Team Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Team>(json);
        }




        // GETTER - SETTER
        public Guid id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = Util.CorrectString(value); }
        /* [JsonIgnore]
        public List<Guid> idPlayers { get => _idPlayers; set => _idPlayers = value; }*/
        [JsonIgnore]
        public Guid idCoach { get => _idCoach; set => _idCoach = value; }



        // PARAM
        [JsonIgnore]
        public bool IsComplete { get => (id != Guid.Empty && name != String.Empty); }
    }
}
