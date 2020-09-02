using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBowl_Library;


namespace Back_Server
{
    public static partial class Database
    {
        /// <summary>
        /// All methods used with the class Credential linked to the Database
        /// </summary>
        public static class CREDENTIALS
        {
            /// <summary>
            /// Returns a Credentials given its ID (if it exists)
            /// </summary>
            /// <param name="id"> ID of the CoCredentialsach </param>
            /// <returns> the Credentials of a given ID (if it exists) </returns>
            public static Credentials GetById(Guid id)
            {
                // We iterate through all the profiles
                foreach (Credentials credential in credentials)
                {
                    // If we find a similar Coach in the Database
                    if (credential.id == id)
                    {
                        return credential;
                    }
                }

                // otherwise, we return default Coach
                return new Credentials();
            }
        }

    }
}