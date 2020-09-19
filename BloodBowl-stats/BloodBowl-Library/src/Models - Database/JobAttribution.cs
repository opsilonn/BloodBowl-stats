using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    [Serializable]
    public class JobAttribution
    {
        private Coach _coach;
        private Guid _coachId;
        private Job _job;



        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a JobAttribution
        /// </summary>
        public JobAttribution()
        {
            coach = new Coach();
            _coachId = Guid.NewGuid();
            job = Job.Player;
        }


        /// <summary>
        /// Creates a complete instance of a JobAttribution with according parameters
        /// </summary>
        /// <param name="coach">Coach of the JobAttribution</param>
        /// <param name="job">Job of the Coach of the JobAttribution</param>
        public JobAttribution(Coach coach, Job job)
        {
            this.coach = coach;
            _coachId = coach.id;
            this.job = job;
        }




        // SERIALIZATION

        /// <summary>
        /// Serializes the JobAttribution instance
        /// </summary>
        /// <returns>A JSON string representation of the JobAttribution instance</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Deserializes a string into a JobAttribution instance
        /// </summary>
        /// <param name="json">JSON's string representation of a JobAttribution instance</param>
        /// <returns>Instance representing the JobAttribution translation from the JSON string</returns>
        public static JobAttribution Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<JobAttribution>(json);
        }




        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString()
        {
            string s = String.Format("\tJob Attribution : {0} - {1}\n", idCoach, job);

            return s;
        }




        // GETTER - SETTER
        [JsonIgnore]
        public Coach coach { get => _coach; set => _coach = value; }
        public Guid idCoach { get => _coachId; set => _coachId = value; }
        public Job job { get => _job; set => _job = value; }




        // PARAM
        [JsonIgnore]
        public bool IsComplete
        {
            get => (coach.IsComplete
            );
        }
    }
}