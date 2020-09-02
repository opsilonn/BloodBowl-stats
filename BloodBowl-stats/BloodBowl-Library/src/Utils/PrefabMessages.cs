using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBowl_Library
{
    public static class PrefabMessages
    {
        public static string INCOMPLETE_FIELDS = "INCOMPLETE FIELDS !!";

        public static string INCORRECT_INPUT = "you didn't entered a correct value !!";
        public static string INCORRECT_INPUT_CHARACTER = "you entered a character which wasn't allowed !!";
        public static string INCORRECT_INPUT_SIZE = "your input(s) are too long !!";

        public static string LOGIN_FAILURE = "The Credentials are incorrect : please try again";

        public static string SIGNIN_PASSWORD_DONT_MATCH = "The password and its verification are different : please try again";
        public static string SIGNIN_FAILURE = "There already is a user with your name and / or email !";

        public static string TEAM_CREATION_SUCCESS = "The new Team was successfully created !";
        public static string TEAM_CREATION_FAILURE = "The Team hasn't been created...";

        public static string PLAYER_CREATION_SUCCESS = "The new Player was successfully created !";
        public static string PLAYER_CREATION_FAILURE = "The Player hasn't been created...";

        public static string LEAGUE_CREATION_SUCCESS = "The new League was successfully created !";
        public static string LEAGUE_CREATION_FAILURE = "The League hasn't been created...";

        public static string NOT_ENOUGH_MONEY = "You don't have enough money to buy this !";

        // Maximum Input sizes
        public static int INPUT_MAXSIZE_COACH_ID = 6;
        public static int INPUT_MAXSIZE_COACH_NAME = 25;
        public static int INPUT_MAXSIZE_COACH_PASSWORD = 25;
        public static int INPUT_MAXSIZE_COACH_EMAIL = 50;

        public static int INPUT_MAXSIZE_NAME = 50;
        public static int INPUT_MAXSIZE_DESCRIPTION = 100;

        public static int INPUT_MAXSIZE_PROFILE_CONTENT = 100;
    }
}
