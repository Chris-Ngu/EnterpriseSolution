using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseSolution
{
    class Request
    {
        private string sqlRequest;
        private DateTime time;
        private string username;

        public Request(string username, DateTime time, string sqlRequest)
        {
            this.username = username;
            this.time = time;
            this.sqlRequest = sqlRequest;
        }

        public override string ToString()
        {
            string s1 = "\n" + this.username + "- " + this.time + ": " + this.sqlRequest;
            return s1;
        }
    }
}
