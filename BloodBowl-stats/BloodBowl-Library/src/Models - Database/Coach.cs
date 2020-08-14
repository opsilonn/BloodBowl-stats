using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace BloodBowl_Library
{
    [Serializable]
    public class Coach
    {
        private Guid _id;
        private string _name;
        private string _email;
        // [JsonPropertyAttribute]
        private List<Team> _teams;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Coach
        /// </summary>
        public Coach()
        {
            id = Guid.Empty;
            name = String.Empty;
            email = String.Empty;
            teams = new List<Team>();
        }


        /// <summary>
        /// Creates a new instance of a Coach with according parameters
        /// </summary>
        /// <param name="name">Name of the Coach</param>
        /// <param name="email">Email of the Coach</param>
        public Coach(string name, string email)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.email = email;
            teams = new List<Team>();
        }


        /// <summary>
        /// Creates a complete instance of a Coach with according parameters
        /// </summary>
        /// <param name="id">Id of the Coach</param>
        /// <param name="name">Name of the Coach</param>
        /// <param name="email">Email of the Coach</param>
        /// <param name="teams">List of his Teams/param>
        public Coach(Guid id, string name, string email, List<Team> teams)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.teams = teams;
        }


        /// <summary>
        /// Creates a complete instance of a Coach with according parameters
        /// </summary>
        /// <param name="coachWithPassword">Coach from which the instance inspires from</param>
        public Coach(CoachWithPassword coachWithPassword)
        {
            id = coachWithPassword.id;
            name = coachWithPassword.name;
            email = coachWithPassword.email;
            teams = coachWithPassword.teams;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            string s = String.Format("Coach : {0} - {1} : {2} teams\n",
                id, name, teams.Count);

            foreach (Team team in teams)
            {
                s += "\t" + team.ToString();
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
        /// Returns a Coach instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A Coach instance from a JSON string</returns>
        public static Coach Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Coach>(json);
        }



        // GETTER - SETTER
        public Guid id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = Util.CorrectString(value); }
        public string email { get => _email; set => _email = value; }
        [JsonIgnore]
        public List<Team> teams { get => _teams; set => _teams = value; }


        // PARAMETERS
        [JsonIgnore]
        public bool IsComplete { get => (id != Guid.Empty && name != String.Empty && email != String.Empty); }
    }
}
