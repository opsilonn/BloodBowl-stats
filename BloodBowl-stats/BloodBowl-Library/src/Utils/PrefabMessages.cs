using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBowl_Library
{
    public static class PrefabMessages
    {
        public const string INCOMPLETE_FIELDS = "INCOMPLETE FIELDS !!";

        public const string INCORRECT_INPUT = "you didn't entered a correct value !!";
        public const string INCORRECT_INPUT_CHARACTER = "you entered a character which wasn't allowed !!";
        public const string INCORRECT_INPUT_SIZE = "your input(s) are too long !!";

        public const string LOGIN_FAILURE = "The Credentials are incorrect : please try again";

        public const string SIGNIN_PASSWORD_DONT_MATCH = "The password and its verification are different : please try again";
        public const string SIGNIN_FAILURE = "There already is a user with your name and / or email !";

        public const string TEAM_CREATION_SUCCESS = "The new Team was successfully created !";
        public const string TEAM_CREATION_FAILURE = "The Team hasn't been created...";

        public const string PLAYER_CREATION_SUCCESS = "The new Player was successfully created !";
        public const string PLAYER_CREATION_FAILURE = "The Player hasn't been created...";

        public const string LEAGUE_CREATION_SUCCESS = "The new League was successfully created !";
        public const string LEAGUE_CREATION_FAILURE = "The League hasn't been created...";
        public const string LEAGUE_INVITATION_COACH_SELF = "You cannot invite yourself to a League !";
        public const string LEAGUE_INVITATION_COACH_SUCCESS = "The invitation was successful !";
        public const string LEAGUE_INVITATION_COACH_FAILURE = "The invitation has not worked...";


        public const string LEAGUE_NONE_ARE_AVAILABLE = "It seems you are not a member of any League !\nPlease consider creating your own, or being invited in an existing League";
        public const string LEAGUE_HAS_NO_COACH = "It seems there is no member in this League...";
        public const string TEAM_NONE_ARE_AVAILABLE = "It seems you have no Team !\nPlease consider creating your a Team";
        public const string INVITATION_COACH_NONE = "It seems you have no invitation at the moment...";
        public const string INVITATION_TEAM_NONE = "It seems you have no invitation for this Team at the moment...";

        public const string NOT_ENOUGH_MONEY = "You don't have enough money to buy this !";


        // SELECTION
        public const string SELECTION_SEE_DATA = "See data";
        public const string SELECTION_GO_BACK = "Go back";
        public const string SELECTION_LEAGUE = "Please select a League (last one = leave) : ";
        public const string SELECTION_COACH = "Please select a Coach (last one = leave) : ";
        public const string SELECTION_TEAM = "Please select a Team (last one = leave) : ";
        public const string SELECTION_PLAYER = "Please select a Player (last one = leave) : ";
        public const string SELECTION_JOB = "Please select a Job (last one = leave) : ";
        // SELECTION - PLAYER
        public const string SELECTION_PLAYER_NEW_LEVEL = "New Level !";
        public const string SELECTION_PLAYER_REMOVE = "Remove Player";
        // SELECTION - LEAGUE
        public const string SELECTION_LEAGUE_SEE_MEMBERS = "See Members";
        public const string SELECTION_LEAGUE_INVITE_MEMBERS = "Invite Member";



        // A DEPLACER DANS UTIL (ou créér une nouvelle classe InputSize) !!
        // Maximum Input sizes
        public const int INPUT_MAXSIZE_COACH_ID = 6;
        public const int INPUT_MAXSIZE_COACH_NAME = 25;
        public const int INPUT_MAXSIZE_COACH_PASSWORD = 25;
        public const int INPUT_MAXSIZE_COACH_EMAIL = 50;

        public const int INPUT_MAXSIZE_NAME = 50;
        public const int INPUT_MAXSIZE_DESCRIPTION = 100;

        public const int INPUT_MAXSIZE_PROFILE_CONTENT = 100;
    }
}
