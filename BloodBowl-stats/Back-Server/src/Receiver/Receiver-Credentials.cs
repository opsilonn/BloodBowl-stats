using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBowl_Library;


namespace Back_Server
{
    public partial class Receiver
    {
        /// <summary>
        /// Server-side verification for the Login
        /// Returns an instance of the Profile logged in (if it didn't work, its ID = -1)
        /// </summary>
        /// <param name="credentials">Credentials of the user</param>
        /// <returns> Whether the Login was successful or not</returns>
        public bool LogIn(Credentials credentials)
        {
            // We extract the credential's data
            string name = credentials.name;
            string password = credentials.password;

            // We create a integer representing the ID of the user ;
            // By default, its ID is negative (= not an actual profile)
            Guid ID_toReturn = Guid.Empty;

            // We iterate through all the Database's Profiles
            foreach (Credentials credential in Database.credentials)
            {
                // If we found a match : BINGO
                if (name == credential.name && password == credential.password)
                {
                    // We check that no one with these credentials is already logged
                    if (!credential.isLogged)
                    {
                        // We set the credential's data for the user being logged to TRUE
                        credential.isLogged = true;

                        // We save the ID found
                        ID_toReturn = credential.id;

                        // We save the Coach representing the user's data
                        userCoach = Database.COACH.GetById(ID_toReturn);
                    }

                    // whatever the result is, we end the loop, since we found the credentials, successful or not
                    break;
                }
            }

            // We send the default Profile if no match was found / the correct one if a match was found
            Net.COACH.Send(comm.GetStream(), userCoach);


            // Returns whether we succeeded or not in Logging in
            return ID_toReturn != Guid.Empty;
        }


        /// <summary>
        /// Server-side verification for the Sign-in
        /// Returns an instance of the newly created Coach (if it didn't work, its ID = -1)
        /// </summary>
        /// <param name="coachReceived">Coach's data of the user trying to Sign in</param>
        /// <returns> The newly created Coach (with password), if successful; otherwise, a default one</returns>
        public CoachWithPassword SignIn(CoachWithPassword coachReceived)
        {
            // We extract the profile's data
            string name = coachReceived.name;
            string password = coachReceived.password;
            string email = coachReceived.email;

            // We create a bool repertoring whether or not a Profile already has the same data as the new user
            // By default, this is false
            bool isTaken = false;


            // We iterate through all the Database's Profiles
            foreach (Coach coach in Database.coaches)
            {
                // If we found a match : ERROR !
                if (name == coach.name || email == coach.email)
                {
                    isTaken = true;
                    break;
                }
            }

            // If the Credentials don't exist : we can create a new Coach
            if (!isTaken)
            {
                // We create a new Coach representing the user's data
                userCoach = new Coach(name, email);
            }

            // We send the Coach (Empty id = SignIn failed)
            Net.COACH.Send(comm.GetStream(), userCoach);

            // We return the profile created
            return new CoachWithPassword(userCoach, password);
        }


        /// <summary>
        /// Server-side that handles all the Log out business
        /// </summary>
        public void LogOut()
        {
            // We reset the "isLogged" parameter from the user's credentials
            Database.CREDENTIALS.GetById(userCoach.id).isLogged = false;

            // We reset the user's data
            userCoach = new Coach();
        }
    }
}
