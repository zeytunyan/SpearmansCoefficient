using System;
using System.Net.Sockets;
using Network;
using Spearman;

namespace Server
{
    /// <summary>The class that handles the clientWrapper.</summary>
    class ClientHandler
    {
        // Disposable objects for working with the clientWrapper
        TcpClientWrapper clientWrapper;

        /// <summary>Initializes a new instance of the <see cref="T:Server.ClientHandler"/> class.</summary>
        /// <param name="tcpClient">The TCP clientWrapper.</param>
        public ClientHandler(TcpClient tcpClient)
        {
            clientWrapper = new TcpClientWrapper(tcpClient);
        }

        /// <summary>
        /// Handles the clientWrapper.
        /// Receives data from the clientWrapper and calculates the SpearmanRealization coefficient. Sends it to the clientWrapper.
        /// </summary>
        public void Handle()
        {
            int[][] query = MakeArr(clientWrapper.Recieve());
            string username = clientWrapper.Recieve();

            var spearman = new SpearmanRealization(query[0], query[1]);
            string conclusion = spearman.Conclusion;

            clientWrapper.Send(conclusion);
            clientWrapper.Close();

            var db = new DB();
            db.Insert(username, spearman.Result, conclusion, query);
            db.Dispose();
        }

        /// <summary>
        /// Creates an array of type int, containing arrays with values to calculate the SpearmanRealization coefficient.
        /// </summary>
        /// <param name="strng">A string with the data from the user.</param>
        /// <returns>System.Int32[][].</returns>
        private int[][] MakeArr(string strng)
        {
            char[] chr = { '|' };
            string[] pair = strng.Split(chr, StringSplitOptions.RemoveEmptyEntries);
            char[] chrs = { ',' };
            string[][] stringArr = new string[2][];
            stringArr[0] = pair[0].Split(chrs, StringSplitOptions.RemoveEmptyEntries);
            stringArr[1] = pair[1].Split(chrs, StringSplitOptions.RemoveEmptyEntries);

            int[][] res = new int[2][];
            res[0] = new int[stringArr[0].Length];
            res[1] = new int[stringArr[0].Length];
            for (int i = 0; i < stringArr[0].Length; i++)
            {
                res[0][i] = Convert.ToInt32(stringArr[0][i]);
                res[1][i] = Convert.ToInt32(stringArr[1][i]);
            }
            return res;
        }

        
    }
}
