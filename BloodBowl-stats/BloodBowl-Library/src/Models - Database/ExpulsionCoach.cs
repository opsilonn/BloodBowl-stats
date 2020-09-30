using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    [Serializable]
    public class ExpulsionCoach : Expulsion
    {
        private JobAttribution _expelled;



        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of an ExpulsionCoach
        /// </summary>
        public ExpulsionCoach() : base()
        {
            league = new League();
            expeller = new JobAttribution();
            expelled = new JobAttribution();
        }


        /// <summary>
        /// Creates a complete instance of an ExpulsionCoach with according parameters
        /// </summary>
        /// <param name="league">League of the Expulsion</param>
        /// <param name="expeller">JobAttribution that is expelling</param>
        /// <param name="expelled">JobAttribution that is expelled</param>
        public ExpulsionCoach(League league, JobAttribution expeller, JobAttribution expelled) : base(league, expeller)
        {
            this.league = league;
            this.expeller = expeller;
            this.expelled = expelled;
        }

        // GETTER - SETTER
        [JsonIgnore]
        public JobAttribution expelled { get => _expelled; set => _expelled = value; }
    }
}
