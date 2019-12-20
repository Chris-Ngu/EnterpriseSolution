using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseSolution.Forms
{
    public static class UserCache
    {
        //Do not store password in cache, no need to and big security problem
        private static string username { get; set; }
        private static string email { get; set; }
        private static string personalNotes { get; set; }

    }
}
