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
        private string summary;

        public Request(string username, DateTime time, string sqlRequest, string summary)
        {
            this.username = username;
            this.time = time;
            this.sqlRequest = sqlRequest;
            this.summary = summary;
        }

        public override string ToString()
        {
            return ("\n" + this.username + "- " + this.time + ": " + this.summary);
            
        }
    }
}
