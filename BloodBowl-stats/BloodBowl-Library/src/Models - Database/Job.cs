using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    [Serializable]
    public enum Job
    {
        CEO,
        Admin,
        Writer,
        Player
    }


    public static class JobStuff
    {
        public class JobData
        {
            public string name { get; }
            public bool canManageMember { get; }
            public bool canWriteArticle { get; }


            public JobData(string name, bool canAddPlayer, bool canWriteArticles)
            {
                this.name = name;
                this.canManageMember = canAddPlayer;
                this.canWriteArticle = canWriteArticles;
            }
        }



        /// <summary>
        /// Data of the instance
        /// </summary>
        /// <param name="job">Job we are analysing</param>
        /// <returns>Data of the instance</returns>
        private static JobData data(this Job job)
        {
            switch (job)
            {
                case Job.CEO:
                    return new JobData("CEO", true, true);
                case Job.Admin:
                    return new JobData("Admin", true, true);
                case Job.Writer:
                    return new JobData("Writer", false, true);
                case Job.Player:
                    return new JobData("Player", false, false);

                default:
                    return new JobData("Player", false, false);
            }
        }


        /// <summary>
        /// Name of the instance
        /// </summary>
        /// <param name="job">Job we are analysing</param>
        /// <returns>Name of the instance</returns>
        public static String name(this Job job)
        {
            return job.data().name;
        }


        /// <summary>
        /// If the instance can manage Member
        /// </summary>
        /// <param name="job">Job we are analysing</param>
        /// <returns>If the instance can manage Member</returns>
        public static bool canManageMember(this Job job)
        {
            return job.data().canManageMember;
        }


        /// <summary>
        /// If the instance can write articles
        /// </summary>
        /// <param name="job">Job we are analysing</param>
        /// <returns>If the instance can write articles</returns>
        public static bool canWriteArticle(this Job job)
        {
            return job.data().canWriteArticle;
        }




        /// <summary>
        /// Returns a list of all the Jobs a given Job can offer (if allowed)
        /// </summary>
        /// <param name="job">Job we are analysing</param>
        /// <returns>a list of all the Jobs a given Job can offer (if allowed)</returns>
        public static List<Job> JobsItCanPropose(this Job job)
        {
            // We initialize a list
            List<Job> jobs = new List<Job>();

            // If allowed, we fill the list
            if(job.canManageMember())
            {
                jobs = GetAllJobs().Where(j => j >= job).ToList();
            }

            // Return the list
            return jobs;
        }




        /// <summary>
        /// Returns a list of all the Jobs
        /// </summary>
        /// <returns>A list of all the Jobs</returns>
        public static List<Job> GetAllJobs()
        {
            return Enum.GetValues(typeof(Job)).Cast<Job>().ToList();
        }
    }
}