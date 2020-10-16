using System.Collections.Generic;


namespace Front_Console
{
    public static class Choice_Prefabs
    {
        public static Choice CHOICE_CONNECTION = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
                "Log in",
                "Sign in",
                "Leave the Program"
           }
       );

        public static Choice CHOICE_CONNECTED = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
               "See my Data",
               "Manage my Leagues",
               "Create a new League",
               "Check my invitations",
               "Manage my Teams",
               "Create a new Team",
               "Log out"
           }
       );

        public static Choice CHOICE_LEAGUE = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
               "See League's Data",
               "Go Back"
           }
       );

        public static Choice CHOICE_TEAM = new Choice
       (
           "Please choose an action :",
           new List<string>()
           {
               "See Team's Data",
               "Manage Players",
               "Buy new Player",
               "Delete team",
               "Go Back"
           }
       );

        public static Choice CHOICE_REMOVEPLAYER = new Choice
       (
           "Do you really want to remove this Player ?",
           new List<string>()
           {
               "Yes, remove the Player",
               "No, keep the Player"
           }
       );

        public static Choice CHOICE_INVITATION = new Choice
       (
           "Do you want to accept this invitation ?",
           new List<string>()
           {
               "Accept invitation",
               "Dismiss invitation",
               "Go Back"
           }
       );

        public static Choice CHOICE_TEAM_DELETE = new Choice
       (
           "Are you sure you want to delete this Team ?",
           new List<string>()
           {
               "Yes, delete Team",
               "No, go Back"
           }
       );

        public static Choice CHOICE_COACH_LEAVE_LEAGUE = new Choice
       (
           "Are you sure you want to leave the League ?",
           new List<string>()
           {
               "Yes, leave League",
               "No, go Back"
           }
       );

        public static string CHOICE_TOPIC_JOIN = "Join the Topic";
        public static string CHOICE_TOPIC_LEAVE = "Leave the Topic";
        public static string CHOICE_TOPIC_GOBACK = "Go Back";
    }
}
