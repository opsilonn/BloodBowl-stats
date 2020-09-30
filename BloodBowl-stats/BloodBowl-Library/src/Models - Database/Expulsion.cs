using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodBowl_Library
{
    [Serializable]
    public abstract class Expulsion
    {
        private League _league;
        private JobAttribution _expeller;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of an Expulsion
        /// </summary>
        public Expulsion()
        {
            league = new League();
            expeller = new JobAttribution();
        }

        /// <summary>
        /// Creates a complete instance of an Expulsion
        /// </summary>
        /// <param name="league">League of the Expulsion</param>
        /// <param name="expeller">JobAttribution that is expelling</param>
        public Expulsion(League league, JobAttribution expeller)
        {
            this.league = league;
            this.expeller = expeller;
        }




        // GETTER - SETTER
        [JsonIgnore]
        public League league { get => _league; set => _league = value; }
        [JsonIgnore]
        public JobAttribution expeller { get => _expeller; set => _expeller = value; }
    }
}
