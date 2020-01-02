using ChattingInterfaces;
using System;
using System.Collections.Concurrent;
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
        public ConcurrentDictionary<string, ConnectedClient> _connectedClients = new ConcurrentDictionary<string, ConnectedClient>(); //threadsafe because server is multithreaded

        public int Login(string username)
        {
            foreach(var client in _connectedClients) //Checking if a current user is already logged in
            {
                if (client.Key.ToLower() == username.ToLower())
                {
                    return 1; //yes
                }
            }

            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>(); //Current user trying to connect

            ConnectedClient newClient = new ConnectedClient();
            newClient.connection = establishedUserConnection;
            newClient.UserName = username;

            _connectedClients.TryAdd(username, newClient);

            return 0;
        }

        public void SendMessageToAll(string message, string username)
        {
            foreach(var client in _connectedClients)
            {
                if (client.Key.ToLower() != username.ToLower())
                {
                    client.Value.connection.GetMessage(message, username);
                }
            }
        }
    }
}
