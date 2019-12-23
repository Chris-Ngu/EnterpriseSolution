using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;

namespace EnterpriseSolution
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Startup());
        }

        private async Task Run()
        {
            var service = new DiscoveryService(new BaseClientService.Initializer
            {
                ApplicationName = "updateappointments",
                ApiKey = "ENTER KEY HERE"
            });

            Console.Write("Executing a list request...");
            var result = await service.Apis.List().ExecuteAsync();
            if (result.Items != null)
            {
                foreach(Google.Apis.Discovery.v1.Data.DirectoryList.ItemsData api in result.Items)
                {
                    Console.WriteLine(api.Id + " - " + api.Title);
                }
            }
        }
    }
}
