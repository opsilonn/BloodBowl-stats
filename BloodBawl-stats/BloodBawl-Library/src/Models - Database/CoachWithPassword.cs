using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace BloodBawl_Library
{
    [Serializable]
    public class CoachWithPassword : Coach
    {
        private string _password;

        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a CoachWithPassword
        /// </summary>
        public CoachWithPassword() : base()
        {
            password = String.Empty;
        }


        /// <summary>
        /// Creates a new instance of a CoachWithPassword with according parameters
        /// </summary>
        /// <param name="name">Name of the CoachWithPassword</param>
        /// <param name="email">Email of the CoachWithPassword</param>
        /// <param name="password">Password of the CoachWithPassword</param>
        public CoachWithPassword(string name, string email, string password) : base(name, email)
        {
            this.password = password;
        }


        /// <summary>
        /// Creates a complete instance of a CoachWithPassword with according parameters
        /// </summary>
        /// <param name="id">Id of the CoachWithPassword</param>
        /// <param name="name">Name of the CoachWithPassword</param>
        /// <param name="email">Email of the CoachWithPassword</param>
        /// <param name="password">Password of the CoachWithPassword</param>
        /// <param name="idTeams">List of his Teams Ids/param>
        public CoachWithPassword(Guid id, string name, string password, string email, List<Guid> idTeams) : base(id, name, email, idTeams)
        {
            this.password = password;
        }


        /// <summary>
        /// Creates a complete instance of a CoachWithPassword with a father Coach instance
        /// </summary>
        /// <param name="coach">Coach this instance is taking form of</param>
        public CoachWithPassword(Coach coach) : base(coach.id, coach.name, coach.email, coach.idTeams)
        {
            password = String.Empty;
        }


        /// <summary>
        /// Creates a complete instance of a CoachWithPassword with a father Coach instance
        /// </summary>
        /// <param name="coach">Coach this instance is taking form of</param>
        /// <param name="password">Password of the CoachWithPassword</param>
        public CoachWithPassword(Coach coach, string password) : base(coach.id, coach.name, coach.email, coach.idTeams)
        {
            this.password = password;
        }


        /// <summary>
        /// Creates a complete instance of a CoachWithPassword with a father Coach instance
        /// </summary>
        /// <param name="coach">Coach this instance is taking form of</param>
        /// <param name="credentials">Credentials of the CoachWithPassword</param>
        public CoachWithPassword(Coach coach, Credentials credentials) : base(coach.id, coach.name, coach.email, coach.idTeams)
        {
            this.password = credentials.password;
        }



        /*
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
                s += team.ToString();
            }

            return s;
        }
        */


        /// <summary>
        /// Return a JSON string representing the instance
        /// </summary>
        /// <returns>A JSON string representing the instance</returns>
        public new string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Returns a CoachWithPassword instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A CoachWithPassword instance from a JSON string</returns>
        public new static CoachWithPassword Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<CoachWithPassword>(json);
        }

        // GETTER - SETTER
        // password is the ONLY PROPERTY of the Coach class that has a designated Order, so it will appear last (which is "cleaner" to read)
        [JsonProperty(Order = 1)]
        public string password { get => _password; set => _password = value; }
    }
}
