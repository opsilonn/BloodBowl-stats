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
            public bool canAddPlayer { get; }
            public bool canWriteArticles { get; }


            public JobData(string name, bool canAddPlayer, bool canWriteArticles)
            {
                this.name = name;
                this.canAddPlayer = canAddPlayer;
                this.canWriteArticles = canWriteArticles;
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
        /// If the instance can add Player
        /// </summary>
        /// <param name="job">Job we are analysing</param>
        /// <returns>If the instance can add Player</returns>
        public static bool canAddPlayer(this Job job)
        {
            return job.data().canAddPlayer;
        }


        /// <summary>
        /// If the instance can write articles
        /// </summary>
        /// <param name="job">Job we are analysing</param>
        /// <returns>If the instance can write articles</returns>
        public static bool canWriteArticles(this Job job)
        {
            return job.data().canWriteArticles;
        }
    }
}