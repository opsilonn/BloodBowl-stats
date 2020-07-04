using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBawl_Library
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

        public static string MESSAGE_EMPTY = "This conversation is empty ! Be the first to add a comment !";
        public static string MESSAGE_SUCCESS = "You successfully added a message !";

        public static string TOPIC_CREATION_SUCCESS = "The new Topic was successfully created !";
        public static string TOPIC_CREATION_FAILURE = "The Topic hasn't been created...";

        public static string CHAT_CREATION_SUCCESS = "The new Chat was successfully created !";
        public static string CHAT_CREATION_FAILURE = "The Chat hasn't been created...";
        public static string CHAT_CREATION_2ND_MEMBER_IS_SELF = "You cannot add yourself as the 2nd member of this Chat !";
        public static string CHAT_CREATION_2ND_MEMBER_NOT_FOUND = "Your correspondent's name was not found !";

        public static string NEWCHATMEMBER_SUCCESS = "The new member was successfully added !";
        public static string NEWCHATMEMBER_FAILURE = "The profile hasn't been added...";
        public static string NEWCHATMEMBER_ADD_SELF = "You cannot add yourself as another new member of this Chat !";

        // Maximum Input sizes
        public static int INPUT_MAXSIZE_COACH_ID = 6;
        public static int INPUT_MAXSIZE_COACH_NAME = 25;
        public static int INPUT_MAXSIZE_COACH_PASSWORD = 25;
        public static int INPUT_MAXSIZE_COACH_EMAIL = 50;

        public static int INPUT_MAXSIZE_STRUCTURE_NAME = 50;
        public static int INPUT_MAXSIZE_STRUCTURE_DESCRIPTION = 100;

        public static int INPUT_MAXSIZE_PROFILE_CONTENT = 100;
    }
}
