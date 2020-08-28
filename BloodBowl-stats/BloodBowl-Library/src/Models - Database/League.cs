using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    public class League
    {
        private Guid _id;
        private string _name;
        private Guid _idCreator;
        private List<JobAttribution> _organisation;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a League
        /// </summary>
        public League()
        {
            id = Guid.Empty;
            name = String.Empty;
            idCreator = Guid.Empty;
            organisation = new List<JobAttribution>();
        }


        /// <summary>
        /// Creates a new instance of a League with according parameters
        /// </summary>
        /// <param name="name">Name of the League</param>
        /// <param name="idCreator">Id of the creator of the League</param>
        public League(string name, Guid idCreator)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.idCreator = idCreator;
            this.organisation = new List<JobAttribution>()
            {
                new JobAttribution(idCreator, Job.CEO)
            };
        }


        /// <summary>
        /// Creates a new instance of a League with according parameters
        /// </summary>
        /// <param name="name">Name of the League</param>
        /// <param name="creator">Creator of the League</param>
        public League(string name, Coach creator)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.idCreator = creator.id;
            this.organisation = new List<JobAttribution>()
            {
                new JobAttribution(creator, Job.CEO)
            };
        }


        /// <summary>
        /// Creates a complete instance of a League with according parameters
        /// </summary>
        /// <param name="id">Id of the League</param>
        /// <param name="name">Name of the League</param>
        /// <param name="idCreator">Id of the creator of the League</param>
        /// <param name="organisation">Ids and Jobs of the organisation of the League</param>
        public League(Guid id, string name, Guid idCreator, List<JobAttribution> organisation)
        {
            this.id = id;
            this.name = name;
            this.idCreator = idCreator;
            this.organisation = organisation;
        }



        // SERIALIZATION

        /// <summary>
        /// Serializes the League instance
        /// </summary>
        /// <returns>A JSON string representation of the League instance</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Deserializes a string into a League instance
        /// </summary>
        /// <param name="json">JSON's string representation of a League instance</param>
        /// <returns>Instance representing the League translation from the JSON string</returns>
        public static League Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<League>(json);
        }


        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            string s = String.Format("\tLeague : {0} - {1}\n", id, name);

            return s;
        }


        // GETTER - SETTER
        public Guid id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = Util.CorrectString(value); }
        public Guid idCreator { get => _idCreator; set => _idCreator = value; }
        /*
        [JsonIgnore]
        public Coach creator { get => Database.COACH.GetById(_idCreator); set => _idCreator = value.id; }
        */
        public List<JobAttribution> organisation { get => _organisation; set => _organisation = value; }


        /*
        [JsonIgnore]
        public Dictionary<Coach, Role> organisation {
            get {
                // We initialize a Dictionay
                Dictionary<Coach, Role> dictionary = new Dictionary<Coach, Role>();

                // We iterate through the Coaches
                foreach (KeyValuePair<Guid, Role> kvp in _idsOrganisation)
                {
                    // We get the Coach's data from the Database
                    Coach newCoach = Database.COACH.GetById(kvp.Key);

                    // If its values are complete :
                    if(newCoach.IsComplete)
                    {
                        // We add him to the dictionary
                        dictionary.Add(newCoach, kvp.Value);
                    }
                }

                // We return the dictionary
                return new Dictionary<Coach, Role>();
            }
            set
            {
                // We reset the dictionary
                _idsOrganisation = new Dictionary<Guid, Role>();

                // We iterate through the given values
                foreach (KeyValuePair<Coach, Role> kvp in value)
                {
                    // We add them accordingly to our dictionary (we only keep the ID !)
                    _idsOrganisation.Add(kvp.Key.id, kvp.Value);
                }
            }
        }
        */
    }
}
