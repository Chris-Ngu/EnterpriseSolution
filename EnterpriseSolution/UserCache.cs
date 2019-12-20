using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseSolution
{
    public static class UserCache
    {
        //Do not store password in cache, no need to and big security problem
        private static string username; //Setting: when logged in
        private static string personalNotes; //Setting: When saving text document (this is what is saved to the MySql server)
                                              //Serialize into byte array and insert as BLOB data type or deserialize as xml/Json

        public static void SetUsername(string x)
        {
            username = x;
        }
        public static string GetUsername()
        {
            return username;
        }
    }
}
