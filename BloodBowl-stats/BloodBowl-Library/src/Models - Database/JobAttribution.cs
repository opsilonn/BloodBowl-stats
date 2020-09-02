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
        private Guid _idCoach;
        private Job _job;



        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a JobAttribution
        /// </summary>
        public JobAttribution()
        {
            idCoach = Guid.Empty;
            job = Job.Player;
        }


        /// <summary>
        /// Creates a complete instance of a JobAttribution with according parameters
        /// </summary>
        /// <param name="idCoach">Id of the Coach of the JobAttribution</param>
        /// <param name="job">Job of the Coach of the JobAttribution</param>
        public JobAttribution(Guid idCoach, Job job)
        {
            this.idCoach = idCoach;
            this.job = job;
        }


        /// <summary>
        /// Creates a complete instance of a JobAttribution with according parameters
        /// </summary>
        /// <param name="coach">Coach of the JobAttribution</param>
        /// <param name="job">Job of the Coach of the JobAttribution</param>
        public JobAttribution(Coach coach, Job job)
        {
            this.idCoach = coach.id;
            this.job = job;
        }



        // GETTER - SETTER
        public Guid idCoach { get => _idCoach; set => _idCoach = value; }
        public Job job { get => _job; set => _job = value; }
    }
}