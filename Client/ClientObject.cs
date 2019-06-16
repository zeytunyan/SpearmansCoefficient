using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    /// <summary>A class that simplifies working with a client.</summary>
    class ClientObject
    {
        // Disposable objects for working with the client
        TcpClient client;
        NetworkStream clientStream;
        StreamReader reader;
        StreamWriter writer;

        /// <summary>Initializes a new instance of the <see cref="T:Client.ClientObject"/> class.</summary>
        /// <param name="address">The address.</param>
        /// <param name="port">The port.</param>
        public ClientObject(IPAddress address, int port) {
            this.client = new TcpClient();
            client.Connect(address, port);
            clientStream = client.GetStream();
            reader = new StreamReader(clientStream);
            writer = new StreamWriter(clientStream);
        }

        /// <summary>Sends the specified message to the client.</summary>
        /// <param name="message">The message.</param>
        public void Send(string message)
        {
            if (client.Connected)
            {
                writer.WriteLine(message);
                writer.Flush();
            }
        }

        /// <summary>Recieves message from client.</summary>
        /// <returns>System.String.
        /// Message</returns>
        public string Recieve()
        {
            string responseMessage = "";
            if (client.Connected)
            {
                responseMessage = reader.ReadLine();
            }
            return responseMessage;
        }

        /// <summary>Closes this instance.</summary>
        public void Close()
        {
            clientStream.Close();
            reader.Close();
            writer.Close();                
            client.Close();
        }
    }
}
