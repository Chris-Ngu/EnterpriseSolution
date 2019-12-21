using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChattingServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)] //one instance of the service with multi threaded server
    public class ChattingService : IChattingService
    {
        public void Test(string value)
        {
            Console.WriteLine(value);
        }
    }
}
