using BloodBowl_Library;
using System;


namespace Front_Console
{
    public partial class Client
    {
        // METHODS - CONNECTION

        /// <summary>
        /// Gets the Credentials of the user, and connects him / makes him retry accordingly
        /// </summary>
        private void LogIn()
        {
            bool continueLogin = true;
            string errorMessage = "";

            do
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the LOGIN");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // USERNAME
                Console.Write("\n Please enter your name : ");
                string name = Console.ReadLine();


                // PASSWORD
                Console.Write("\n Please enter your password : ");
                string password = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && password.Length == 0)
                {
                    continueLogin = false;
                }
                // If at least one field is empty : ERROR
                else if (name.Length == 0 || password.Length == 0)
                {
                    errorMessage = PrefabMessages.INCOMPLETE_FIELDS;
                }
                // If at least one field is too long : ERROR
                else if (name.Length > PrefabMessages.INPUT_MAXSIZE_COACH_NAME || password.Length > PrefabMessages.INPUT_MAXSIZE_COACH_PASSWORD)
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!Util.CorrectInput(name) || !Util.CorrectInput(password))
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : verify with the server
                else
                {
                    // Sending the credentials
                    Instructions instruction = Instructions.LogIn;
                    Object content = new Credentials(name, password);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // Receiving the response ID
                    userData = Net.COACH.Receive(comm.GetStream());

                    // Match found, we proceed forward
                    if (userData.IsComplete)
                    {
                        continueLogin = false;
                    }
                    // If the ID is Empty : there was no match found, we reset the login
                    else
                    {
                        errorMessage = PrefabMessages.LOGIN_FAILURE;
                    }
                }
            }
            while (continueLogin);
        }


        /// <summary>
        /// Gets the newly created Coach's data of the user, and connects him / makes him retry accordingly
        /// </summary>
        private void SignIn()
        {
            bool continueSignin = true;
            string errorMessage = "";

            while (continueSignin)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the SIGN-IN");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // USERNAME
                Console.Write("\n Please enter your username : ");
                string name = Console.ReadLine();


                // PASSWORD
                Console.Write("\n Please enter your password : ");
                string password = Console.ReadLine();


                // PASSWORD - VERIFICATION
                Console.Write("\n Please verify your password : ");
                string passwordVerif = Console.ReadLine();


                // EMAIL
                Console.Write("\n Please enter your email : ");
                string email = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && password.Length == 0 && passwordVerif.Length == 0 && email.Length == 0)
                {
                    continueSignin = false;
                }
                // If at least one field is empty : ERROR
                else if (name.Length == 0 || password.Length == 0 || passwordVerif.Length == 0 || email.Length == 0)
                {
                    errorMessage = PrefabMessages.INCOMPLETE_FIELDS;
                }
                // The password and its verification do not match : ERROR
                else if (password != passwordVerif)
                {
                    errorMessage = PrefabMessages.SIGNIN_PASSWORD_DONT_MATCH;
                }
                // NOW, we don't have to verify the PasswordVerif field anymore !
                // If at least one field is too long : ERROR
                else if (name.Length > PrefabMessages.INPUT_MAXSIZE_COACH_NAME
                    || password.Length > PrefabMessages.INPUT_MAXSIZE_COACH_PASSWORD
                    || email.Length > PrefabMessages.INPUT_MAXSIZE_COACH_EMAIL)
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!Util.CorrectInput(name) || !Util.CorrectInput(password) || !Util.CorrectInput(email))
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : verify with the server
                else
                {
                    // Sending the new Coach (with password !)
                    Instructions instruction = Instructions.SignIn;
                    Object content = new CoachWithPassword(name, password, email);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // We get the Coach data back
                    // CORRECT :  regular data
                    // INCORRECT : default data
                    userData = Net.COACH.Receive(comm.GetStream());


                    // the data is complete : successful Sign in
                    if (userData.IsComplete)
                    {
                        continueSignin = false;
                    }
                    // If the data is incomplete : the profile's name and/or email is already taken, we reset the sign-in
                    {
                        errorMessage = PrefabMessages.SIGNIN_FAILURE;
                    }
                }
            }
        }


        /// <summary>
        /// Ensures the user Logs Out of the Softare (but remains to the Log In / Sign In menu)
        /// </summary>
        private void LogOut()
        {
            // Display a Farewell message
            CONSOLE.WriteLine(ConsoleColor.Blue, "\n\n     Goodbye ! We hope to see you soon :)\n\n");

            // Resetting the user's Data : no user is logged in anymore
            userData = new Coach();

            // Warning the server we log out
            Instructions instruction = Instructions.LogOut;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

            // Wait for input
            CONSOLE.WaitForInput();
        }
    }
}
