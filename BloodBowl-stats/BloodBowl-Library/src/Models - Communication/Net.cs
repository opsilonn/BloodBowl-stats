using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;


namespace BloodBowl_Library
{
    public class Net
    {
        public static class COMMUNICATION
        {
            /// <summary>
            /// Sends a Communication
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Communication to send </param>
            public static void Send(Stream s, Communication msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// 
            /// Receives a Communication
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Communication </returns>
            public static Communication Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Communication)bf.Deserialize(s);
            }
        }

        public static class STRING
        {
            /// <summary>
            /// Sends a String
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> String to send </param>
            public static void Send(Stream s, string msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a String
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized String </returns>
            public static string Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (string)bf.Deserialize(s);
            }
        }

        public static class INT
        {
            /// <summary>
            /// Sends an integer
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Integer to send </param>
            public static void Send(Stream s, int msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives an integer
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Int </returns>
            public static int Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (int)bf.Deserialize(s);
            }
        }

        public static class BOOL
        {
            /// <summary>
            /// Sends a boolean
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Boolean to send </param>
            public static void Send(Stream s, bool msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a boolean
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized boolean </returns>
            public static bool Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (bool)bf.Deserialize(s);
            }
        }

        public static class GUID
        {
            /// <summary>
            /// Sends a Guid
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Guid to send </param>
            public static void Send(Stream s, Guid msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a Guid
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Guid </returns>
            public static Guid Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Guid)bf.Deserialize(s);
            }
        }

        public static class COACH
        {
            /// <summary>
            /// Sends a Coach
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Coach to send </param>
            public static void Send(Stream s, Coach msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a Coach
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Coach </returns>
            public static Coach Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Coach)bf.Deserialize(s);
            }



            /// <summary>
            /// Sends a List of Coaches
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="coaches"> List of Coaches to send </param>
            public static void SendMany(Stream s, List<Coach> coaches)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, coaches);
            }


            /// <summary>
            /// Receives a List of Coaches
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns>A list of Teams</returns>
            public static List<Coach> ReceiveMany(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Coach>)bf.Deserialize(s);
            }


            /// <summary>
            /// Gets a Coach by its id
            /// </summary>
            /// <param name="id"> id of the Coach seeked </param>
            /// <returns> The Coach with the id from the Database </returns>
            public static Coach GetByID(TcpClient comm, Guid id)
            {
                // Asking for a Coach
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Coach_GetById, id));

                // We return the Coach received
                return Net.COACH.Receive(comm.GetStream());
            }
        }

        public static class TEAM
        {
            /// <summary>
            /// Sends a Team
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Team to send </param>
            public static void Send(Stream s, Team msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a Team
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Team </returns>
            public static Team Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Team)bf.Deserialize(s);
            }



            /// <summary>
            /// Sends a List of Teams
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="teams"> List of Coaches to send </param>
            public static void SendMany(Stream s, List<Team> teams)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, teams);
            }


            /// <summary>
            /// Receives a List of Teams
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns>A list of Teams</returns>
            public static List<Team> ReceiveMany(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Team>)bf.Deserialize(s);
            }


            /// <summary>
            /// Gets a Team by its id
            /// </summary>
            /// <param name="id"> id of the Team seeked </param>
            /// <returns> The Team with the id from the Database </returns>
            public static Team GetByID(TcpClient comm, Guid id)
            {
                // Asking for a Team
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Team_GetById, id));

                // We return the Team received
                return Net.TEAM.Receive(comm.GetStream());
            }
        }


        public static class EFFECT
        {
            /// <summary>
            /// Sends an Perk
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="effect"> Perk to send </param>
            public static void Send(Stream s, Perk effect)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, effect);
            }


            /// <summary>
            /// Receives an Perk
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Communication </returns>
            public static Perk Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Perk)bf.Deserialize(s);
            }
        }



        //// UNIFORM MODEL
        /*
        /// <summary>
        /// Sends a Message
        /// </summary>
        /// <param name="s"> Stream </param>
        /// <param name="msg"> Message to send </param>
        public static void Send(Stream s, Object msg)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, msg);
        }


        /// <summary>
        /// Receives a Message
        /// </summary>
        /// <param name="s"> Stream </param>
        /// <returns> The serialized Message </returns>
        public static Object Receive(Stream s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            return (Object)bf.Deserialize(s);
        }
        */
    }
}