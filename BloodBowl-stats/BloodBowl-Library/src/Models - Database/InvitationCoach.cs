using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    [Serializable]
    public class InvitationCoach : Invitation
    {
        private Coach _invited;
        private Guid _idInvited;
        private Job _job;



        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of an Invitation
        /// </summary>
        public InvitationCoach() : base()
        {
            invited = new Coach();
            idInvitor = Guid.Empty;
            job = Job.Player;
        }


        /// <summary>
        /// Creates a new instance of an Invitation with according parameters
        /// </summary>
        /// <param name="league">League of the Invitation</param>
        /// <param name="invitor">Invitor of the Invitation</param>
        /// <param name="invited">Coach that is invited</param>
        /// <param name="job">Job that the invited Coach is elected to</param>
        public InvitationCoach(League league, Coach invitor, Coach invited, Job job): base(league, invitor)
        {
            this.invited = invited;
            idInvited = invited.id;
            this.job = job;
        }


        /// <summary>
        /// Creates a complete instance of a JobAttribution with according parameters
        /// </summary>
        /// <param name="date">Date of the Invitation</param>
        /// <param name="league">League of the Invitation</param>
        /// <param name="invitor">Invitor of the Invitation</param>
        /// <param name="invited">Coach that is invited</param>
        /// <param name="job">Job that the invited Coach is elected to</param>
        public InvitationCoach(DateTime date, League league, Coach invitor, Coach invited, Job job) : base(date, league, invitor)
        {
            this.invited = invited;
            idInvited = invited.id;
            this.job = job;
        }



        // SERIALIZATION

        /// <summary>
        /// Serializes the Invitation instance
        /// </summary>
        /// <returns>A JSON string representation of the Invitation instance</returns>
        public override string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Deserializes a string into a Invitation instance
        /// </summary>
        /// <param name="json">JSON's string representation of a Invitation instance</param>
        /// <returns>Instance representing the Invitation translation from the JSON string</returns>
        public static Invitation Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Invitation>(json);
        }




        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            string s = String.Format("\tInvitation Coach: {0} is invited by {1} to {2}\n", invited.name, invitor.id, league.name);

            return s;
        }



        // GETTER - SETTER
        [JsonIgnore]
        public Coach invited
        {
            get => _invited;
            set
            {
                _invited = value;
                _idInvited = _invited.id;
            }
        }
        public Guid idInvited { get => _idInvited; set => _idInvited = value; }
        public Job job { get => _job; set => _job = value; }





        // PARAM
        [JsonIgnore]
        public bool IsComplete
        {
            get => (
                idLeague != Guid.Empty
                && idInvitor != Guid.Empty
                && idInvited != Guid.Empty
                );
        }
    }
}