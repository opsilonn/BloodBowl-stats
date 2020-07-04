using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBawl_Library
{
    [Serializable]
    public class Credentials
    {
        private Guid _id;
        private string _name;
        private string _password;


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Credentials
        /// </summary>
        public Credentials()
        {
            id = Guid.Empty;
            name = String.Empty;
            password = String.Empty;
        }


        /// <summary>
        /// Creates a new instance of a Credentials with according parameters
        /// </summary>
        /// <param name="name">Name of the Credentials</param>
        /// <param name="password">Password of the Credentials</param>
        public Credentials(string name, string password)
        {
            id = Guid.Empty;
            this.name = name;
            this.password = password;
        }

        /// <summary>
        /// Creates a complete instance of a Credentials with according parameters
        /// </summary>
        /// <param name="id">Id of the Credentials</param>
        /// <param name="name">Name of the Credentials</param>
        /// <param name="password">Password of the Credentials</param>
        public Credentials(Guid id, string name, string password)
        {
            this.id = id;
            this.name = name;
            this.password = password;
        }




        // SERIALIZATION

        /// <summary>
        /// Return a JSON string representing the Credentials instance
        /// </summary>
        /// <returns>A JSON string representing the Credentials instance</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Returns a Credentials instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A Credentials instance from a JSON string</returns>
        public static Credentials Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Credentials>(json);
        }


        // GETTER / SETTER
        public Guid id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = value; }
        public string password { get => _password; set => _password = value; }
        [JsonIgnore]
        public bool IsComplete { get => (id != Guid.Empty && name != String.Empty && password != String.Empty); }

        public override string ToString()
        {
            return "Credentials : " + name + " - " + password;
        }
    }
}
