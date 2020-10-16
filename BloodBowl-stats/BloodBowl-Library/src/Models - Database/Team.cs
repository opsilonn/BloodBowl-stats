using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    [Serializable]
    public class Team
    {
        private Guid _id;
        private DateTime _dateCreation;
        private string _name;
        private string _description;
        private Race _race;
        private int _money;
        private List<Player> _players;
        private Coach _coach;
        private Guid _idCoach;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Team
        /// </summary>
        public Team()
        {
            id = Guid.Empty;
            dateCreation = DateTime.MinValue;
            name = String.Empty;
            description = String.Empty;
            race = Race.Humans;
            money = 0;
            players = new List<Player>();
            coach = new Coach();
        }


        /// <summary>
        /// Creates a new instance of a Team with according parameters
        /// </summary>
        /// <param name="name">Name of the Team</param>
        /// <param name="description">Description of the Team</param>
        /// <param name="race">Race of the Team</param>
        /// <param name="coach">Coach of the Team</param>
        public Team(string name, string description, Race race, Coach coach)
        {
            id = Guid.NewGuid();
            dateCreation = DateTime.Now;
            this.name = name;
            this.description = description;
            this.race = race;
            money = 1000;
            players = new List<Player>();
            this.coach = coach;
        }


        /// <summary>
        /// Creates a complete instance of a Team with according parameters
        /// </summary>
        /// <param name="id">Id of the Team</param>
        /// <param name="dateCreation">Date of creation of the instance</param>
        /// <param name="name">Name of the Team</param>
        /// <param name="description">Description of the Team</param>
        /// <param name="race">Race of the Team</param>
        /// <param name="money">Money of the Team</param>
        /// <param name="players">Players of the Team</param>
        /// <param name="coach">Coach of the Team</param>
        public Team(Guid id, DateTime dateCreation, string name, string description, Race race, int money, List<Player> players, Coach coach)
        {
            this.id = id;
            this.dateCreation = dateCreation;
            this.name = name;
            this.description = description;
            this.race = race;
            this.money = money;
            this.players = players;
            this.coach = coach;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            string s = String.Format("Team : {0} - {1} - {2} players\n", name, race.name(), players.Count);

            foreach (Player player in players)
            {
                s += "\t" + player.ToString();
            }
            return s + "\n";
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
        public DateTime dateCreation { get => _dateCreation; set => _dateCreation = value; }
        public string name { get => _name; set => _name = value; }
        public string description { get => _description; set => _description = value; }
        public Race race { get => _race; set => _race = value; }
        public int money { get => _money; set => _money = value; }
        public List<Player> players { get => _players; set => _players = value; }
        [JsonIgnore]
        public Coach coach
        {
            get => _coach;
            set
            {
                _coach = value;
                _idCoach = _coach.id;
            }
        }
        public Guid idCoach { get => _idCoach; set => _idCoach = value; }



        // PARAM
        [JsonIgnore]
        public bool IsComplete { get => (
                id != Guid.Empty
                && dateCreation != DateTime.MinValue
                && name != String.Empty
                );
        }

        [JsonIgnore]
        public int value { get => players.Select(player => player.value).Sum(); }
        [JsonIgnore]
        public string valueToString { get => string.Format("{0:#,0, K}", value); }
        

        [JsonIgnore]
        public string shortDescription { get => name + "\n\t" + valueToString + " - " + race.name(); }
    }
}