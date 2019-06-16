using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Begin to listen
            TcpListener listener = new TcpListener(IPAddress.Any, 8001);
            listener.Start();
            Console.WriteLine("Server started!");

            try
            {
                // Accept clients
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientHandler handler = new ClientHandler(client);

                    // Processing each client in a separate thread
                    Thread clientThread = new Thread(new ThreadStart(handler.Handle));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
