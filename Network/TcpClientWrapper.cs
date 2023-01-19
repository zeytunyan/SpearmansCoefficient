using System.Net.Sockets;
using System.Net;
using System.IO;
using System;

namespace Network
{
    /// <summary>A class that simplifies working with a client.</summary>
    public class TcpClientWrapper
    {
        // Disposable objects for working with the client
        TcpClient client;
        NetworkStream clientStream;
        StreamReader reader;
        StreamWriter writer;

        /// <summary>Initializes a new instance of the <see cref="T:Network.TcpClientWrapper"/> class.</summary>
        /// <param name="address">The address.</param>
        /// <param name="port">The port.</param>
        public TcpClientWrapper(IPAddress address, int port)
        {
            client = new TcpClient();
            client.Connect(address, port);
            CreateStreams();
        }

        /// <summary>Initializes a new instance of the <see cref="T:Network.TcpClientWrapper"/> class.</summary>
        /// <param name="address">The address.</param>
        /// <param name="port">The port.</param>
        public TcpClientWrapper(TcpClient tcpClient)
        {
            client = tcpClient;
            CreateStreams();
        }

        private void CreateStreams()
        {
            clientStream = client.GetStream();
            reader = new StreamReader(clientStream);
            writer = new StreamWriter(clientStream);
        }

        /// <summary>Sends the specified message to the client.</summary>
        /// <param name="message">The message.</param>
        public void Send(string message)
        {
            ThrowExIfClientIsNotConnected();
            writer.WriteLine(message);
            writer.Flush();
        }

        /// <summary>Recieves message from client.</summary>
        /// <returns>System.String.
        /// Message</returns>
        public string Recieve()
        {
            ThrowExIfClientIsNotConnected();
            return reader.ReadLine();
        }

        private void ThrowExIfClientIsNotConnected()
        {
            if(!client.Connected) 
                throw new Exception("Client is not connected");
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