using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    [Serializable]
    public class League
    {
        private Guid _id;
        private DateTime _dateCreation;
        private string _name;
        private Guid _idCreator;
        private List<JobAttribution> _members;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a League
        /// </summary>
        public League()
        {
            id = Guid.Empty;
            dateCreation = DateTime.MinValue;
            name = String.Empty;
            idCreator = Guid.Empty;
            members = new List<JobAttribution>();
        }


        /// <summary>
        /// Creates a new instance of a League with according parameters
        /// </summary>
        /// <param name="name">Name of the League</param>
        /// <param name="idCreator">Id of the creator of the League</param>
        public League(string name, Guid idCreator)
        {
            id = Guid.NewGuid();
            dateCreation = DateTime.Now;
            this.name = name;
            this.idCreator = idCreator;
            this.members = new List<JobAttribution>()
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
            dateCreation = DateTime.MinValue;
            this.name = name;
            this.idCreator = creator.id;
            this.members = new List<JobAttribution>()
            {
                new JobAttribution(creator, Job.CEO)
            };
        }


        /// <summary>
        /// Creates a complete instance of a League with according parameters
        /// </summary>
        /// <param name="id">Id of the League</param>
        /// <param name="dateCreation">DateTime of creation of the League</param>
        /// <param name="name">Name of the League</param>
        /// <param name="idCreator">Id of the creator of the League</param>
        /// <param name="members">Ids and Jobs of the members of the League</param>
        public League(Guid id, DateTime dateCreation, string name, Guid idCreator, List<JobAttribution> members)
        {
            this.id = id;
            this.dateCreation = dateCreation;
            this.name = name;
            this.idCreator = idCreator;
            this.members = members;
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
        public DateTime dateCreation { get => _dateCreation; set => _dateCreation = value; }
        public string name { get => _name; set => _name = Util.CorrectString(value); }
        public Guid idCreator { get => _idCreator; set => _idCreator = value; }
        public List<JobAttribution> members { get => _members; set => _members = value; }


        // PARAM
        [JsonIgnore]
        public bool IsComplete
        {
            get => (
                id != Guid.Empty
                && dateCreation != DateTime.MinValue
                && name != String.Empty
            );
        }
    }
}
