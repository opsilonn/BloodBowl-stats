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


        public static string LEAGUE_NONE_ARE_AVAILABLE = "It seems you are not a member of any League !\nPlease consider creating your own, or being invited in an existing League";
        public static string TEAM_NONE_ARE_AVAILABLE = "It seems you have no Team !\nPlease consider creating your a Team";

        public static string NOT_ENOUGH_MONEY = "You don't have enough money to buy this !";


        // SELECTION
        public static string SELECTION_GO_BACK = "Go back";
        public static string SELECTION_LEAGUE = "please Select a League (last one = leave) : ";
        public static string SELECTION_TEAM = "please Select a Team (last one = leave) : ";
        public static string SELECTION_PLAYER = "please Select a Player (last one = leave) : ";
        // SELECTION - PLAYER'S NEW LEVEL
        public static string SELECTION_SEE_DATA = "See Data";
        public static string SELECTION_NEW_LEVEL = "New Level !";
        public static string SELECTION_REMOVE_PLAYER = "Remove Player";


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
