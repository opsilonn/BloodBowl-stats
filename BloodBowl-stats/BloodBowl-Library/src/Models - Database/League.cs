using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private Coach _creator;
        private Guid _creatorId;
        private List<JobAttribution> _members;
        // private List<Invitation> _invitations;
        private List<InvitationCoach> _invitedCoaches;
        // private List<Team> _teams;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a League
        /// </summary>
        public League()
        {
            id = Guid.Empty;
            dateCreation = DateTime.MinValue;
            name = String.Empty;
            creator = new Coach();
            members = new List<JobAttribution>();
            invitedCoaches = new List<InvitationCoach>();
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
            this.creator = creator;
            this.members = new List<JobAttribution>()
            {
                new JobAttribution(creator, Job.CEO)
            };
            invitedCoaches = new List<InvitationCoach>();
        }


        /// <summary>
        /// Creates a complete instance of a League with according parameters
        /// </summary>
        /// <param name="id">Id of the League</param>
        /// <param name="dateCreation">DateTime of creation of the League</param>
        /// <param name="name">Name of the League</param>
        /// <param name="creator">Creator of the League</param>
        /// <param name="members">Ids and Jobs of the members of the League</param>
        /// <param name="invitations">Invitations of new members and Teams to the League</param>
        public League(Guid id, DateTime dateCreation, string name, Coach creator, List<JobAttribution> members, List<InvitationCoach> invitedCoaches)
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
        public string name { get => _name; set => _name = Util.ConvertToCorrectString(value); }
        [JsonIgnore]
        public Coach creator { get => _creator; set { _creator = value; _creatorId = _creator.id; } }
        public Guid idCreator { get => _creatorId; set => _creatorId = value; }
        public List<JobAttribution> members { get => _members; set => _members = value; }
        public List<InvitationCoach> invitedCoaches { get => _invitedCoaches; set => _invitedCoaches = value; }

        // _members.OrderBy(member => member.job).ToList()


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


        /// <summary>
        /// Returns whether a given Coach is a member of this League instance
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <returns>Whether a given Coach is a member of this League instance</returns>
        public bool ContainsMember(Guid memberId)
        {
            return members.Count(member => (member.idCoach == memberId)) != 0;
        }


        /// <summary>
        /// Returns a Member from the instance, given its Id
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <returns>A Member from the instance, given its Id (returns default, if not found)</returns>
        public JobAttribution GetMember(Guid memberId)
        {
            // We get the First match we get
            JobAttribution ja = members.FirstOrDefault(member => (member.idCoach == memberId));

            // We check if it is not null (if it is, return default instance)
            return (ja != null) ? ja : new JobAttribution();
        }


        /// <summary>
        /// Removes a member from the instance
        /// </summary>
        /// <param name="member">Member to remove</param>
        public void RemoveMember(JobAttribution member)
        {
            members.Remove(member);
        }




        /// <summary>
        /// Accepts an InvitationCoach
        /// </summary>
        /// <param name="invitationCoach">InvitationCoach that has been accepted</param>
        public void AcceptInvitationCoach(InvitationCoach invitationCoach)
        {
            // First - we add the invited as a member
            JobAttribution ja = new JobAttribution(invitationCoach.invited, invitationCoach.job);
            members.Add(ja);

            // Second - remove all similar invitations
            invitedCoaches.RemoveAll(invit => invit.idInvited == invitationCoach.idInvited);
        }


        /// <summary>
        /// Declines an InvitationCoach
        /// </summary>
        /// <param name="invitationCoach">InvitationCoach that has been declined</param>
        public void RefuseInvitationCoach(InvitationCoach invitationCoach)
        {
            // Second - remove the invitation
            invitedCoaches.RemoveAll(invit =>
                invit.idInvited == invitationCoach.idInvited
                && invit.idInvitor == invitationCoach.idInvitor
                && invit.job == invitationCoach.job
                );
        }


        /// <summary>
        /// Returns whether a given Coach is a member of this League instance
        /// </summary>
        /// <param name="icReceived">InvitationCoach received</param>
        /// <returns>Whether a given Coach is a member of this League instance</returns>
        public bool ContainsSimilarInvitedCoach(InvitationCoach icReceived)
        {
            return invitedCoaches.Where(ia => (ia.idInvited == icReceived.idInvited && ia.job == icReceived.job)).Any();
        }
    }
}