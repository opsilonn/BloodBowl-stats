using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    [Serializable]
    public enum Job
    {
        CEO,
        Admin,
        Commisar,
        Writer,
        Player,
        BlackListed
    }


    public static class JobStuff
    {
        public class JobData
        {
            public string name { get; }
            public bool canCreateCompetition { get; }
            public bool canManageMember { get; }
            public bool canVerifyResult { get; }
            public bool canWriteArticle { get; }


            public JobData(string name, bool canCreateCompetition, bool canManageMember, bool canVerifyResult, bool canWriteArticle)
            {
                this.name = name;
                this.canCreateCompetition = canCreateCompetition;
                this.canManageMember = canManageMember;
                this.canVerifyResult = canVerifyResult;
                this.canWriteArticle = canWriteArticle;
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
                    return new JobData("CEO", true, true, true, true);
                case Job.Admin:
                    return new JobData("Admin", true, true, true, true);
                case Job.Commisar:
                    return new JobData("Commissar", false, true, true, true);
                case Job.Writer:
                    return new JobData("Writer", false, false, false, true);
                case Job.Player:
                    return new JobData("Player", false, false, false, false);
                case Job.BlackListed:
                    return new JobData("Black-Listed", false, false, false, false);

                default:
                    return new JobData("Player", false, false, false, false);
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
                // All the jobs below the one given
                // - we exclude all things above the CEO
                // - we exclude all things below the black list
                jobs = GetAllJobs().Where(j =>
                    Job.CEO < j
                    && job <= j
                    && j < Job.BlackListed).ToList();
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