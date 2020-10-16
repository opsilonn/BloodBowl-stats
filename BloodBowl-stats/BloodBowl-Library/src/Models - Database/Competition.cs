using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBowl_Library
{
    [Serializable]
    public class Competition
    {
        private Guid _id;
        private DateTime _dateCreation;
        private string _name;
        private JobAttribution _creator;
        private Guid _creatorId;
        private List<JobAttribution> _members;
        private List<Team> _teams;
        private List<InvitationCoach> _invitedCoaches;
        // private List<InvitationTeam> _invitedTeams;
        private League _league;
        private Guid _idLeague;



        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Competition
        /// </summary>
        public Competition()
        {
            id = Guid.Empty;
            dateCreation = DateTime.MinValue;
            name = String.Empty;
            creator = new JobAttribution();
            members = new List<JobAttribution>();
            invitedCoaches = new List<InvitationCoach>();
            league = new League();
        }


        /// <summary>
        /// Creates a new instance of a Competition with according parameters
        /// </summary>
        /// <param name="name">Name of the Competition</param>
        /// <param name="creator">Creator of the Competition</param>
        public Competition(string name, JobAttribution creator, League league)
        {
            id = Guid.NewGuid();
            dateCreation = DateTime.MinValue;
            this.name = name;
            this.creator = creator;
            this.members = new List<JobAttribution>()
            {
                creator
            };
            invitedCoaches = new List<InvitationCoach>();
            this.league = league;
        }


        /// <summary>
        /// Creates a complete instance of a Competition with according parameters
        /// </summary>
        /// <param name="id">Id of the Competition</param>
        /// <param name="dateCreation">DateTime of creation of the Competition</param>
        /// <param name="name">Name of the Competition</param>
        /// <param name="creator">Creator of the Competition</param>
        /// <param name="members">Ids and Jobs of the members of the Competition</param>
        /// <param name="invitedCoaches">Invitations of new members and Teams to the Competition</param>
        public Competition(Guid id, DateTime dateCreation, string name, JobAttribution creator, List<JobAttribution> members, List<InvitationCoach> invitedCoaches)
        {
            this.id = id;
            this.dateCreation = dateCreation;
            this.name = name;
            this.creator = creator;
            this.members = members;
            this.invitedCoaches = invitedCoaches;
        }



        // SERIALIZATION

        /// <summary>
        /// Serializes the Competition instance
        /// </summary>
        /// <returns>A JSON string representation of the Competition instance</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Deserializes a string into a Competition instance
        /// </summary>
        /// <param name="json">JSON's string representation of a Competition instance</param>
        /// <returns>Instance representing the Competition translation from the JSON string</returns>
        public static Competition Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Competition>(json);
        }


        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            string s = String.Format("\tCompetition : {0} - {1}\n", id, name);

            return s;
        }


        // GETTER - SETTER
        public Guid id { get => _id; set => _id = value; }
        public DateTime dateCreation { get => _dateCreation; set => _dateCreation = value; }
        public string name { get => _name; set => _name = Util.ConvertToCorrectString(value); }
        [JsonIgnore]
        public JobAttribution creator { get => _creator; set { _creator = value; _creatorId = _creator.coach.id; } }
        public Guid idCreator { get => _creatorId; set => _creatorId = value; }
        public List<JobAttribution> members { get => _members; set => _members = value; }
        public List<InvitationCoach> invitedCoaches { get => _invitedCoaches; set => _invitedCoaches = value; }
        [JsonIgnore]
        public League league
        {
            get => _league;
            set
            {
                _league = value;
                _idLeague = _league.id;
            }
        }
        public Guid idLeague { get => _idLeague; set => _idLeague = value; }


        // PARAM
        [JsonIgnore]
        public bool IsComplete
        {
            get => (
                id != Guid.Empty
                && name != String.Empty
            );
        }
    }
}