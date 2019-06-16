using System;
using System.Net.Sockets;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Server
{
    /// <summary>The class that handles the client.</summary>
    class ClientHandler
    {
        // Disposable objects for working with the client
        TcpClient client;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;

        /// <summary>Initializes a new instance of the <see cref="T:Server.ClientHandler"/> class.</summary>
        /// <param name="tcpClient">The TCP client.</param>
        public ClientHandler(TcpClient tcpClient)
        {
            this.client = tcpClient;
        }

        /// <summary>
        /// Handles the client.
        /// Receives data from the client and calculates the Spearman coefficient. Sends it to the client.
        /// </summary>
        public void Handle()
        {
            this.stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            int[][] query = MakeArr(this.Recieve());
            string username = this.Recieve();

            Spearman spearman = new Spearman(query[0], query[1]);
            string conclusion = spearman.Conclusion();

            this.Send(conclusion);
            this.Close();

            this.InsertToDB(username, spearman.Result, conclusion, query);
        }

        /// <summary>Sends the specified message to a client.</summary>
        /// <param name="message">The message.</param>
        private void Send(string message)
        {
            if (client.Connected)
            {
                writer.WriteLine(message);
                writer.Flush();
            }
        }

        /// <summary>Recieves message from client</summary>
        /// <returns>
        /// System.String.
        /// </returns>
        private string Recieve()
        {
            string responseMsg = "";
            if (client.Connected)
            {
                responseMsg = reader.ReadLine();
            }
            return responseMsg;
        }

        /// <summary>
        /// Closes all disposable objects.
        /// </summary>
        private void Close()
        {
            stream.Close();
            reader.Close();
            writer.Close();
            client.Close();
        }

        /// <summary>
        /// Creates an array of type int, containing arrays with values to calculate the Spearman coefficient.
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

        /// <summary>Inserts data to database.</summary>
        /// <param name="uname">  Username.</param>
        /// <param name="res"> Calculated Spearman's coefficient.</param>
        /// <param name="concl">Conclusion based on Spearman's coefficient.</param>
        /// <param name="qr">  Array with user-entered attributes .</param>
        private void InsertToDB(string uname, double res, string concl, int[][]qr)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(LocalDB)\MSSQLLocalDB";
            builder.AttachDBFilename = @"C:\Users\Георгий\source\repos\Spearmans_RHO\Server\Spearman.mdf";
            builder.IntegratedSecurity = true;

            using (IDbConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SpearmanClassesDataContext context = new SpearmanClassesDataContext(connection))
                {
                    Results result = new Results
                    {
                        Name = uname,
                        Coeff = Convert.ToDecimal(res),
                        Concl = concl,
                        Time = DateTime.Now
                    };
                    context.Results.InsertOnSubmit(result);
                    context.SubmitChanges();

                    Variables[] variables = new Variables[qr[0].Length];
                    for (int i = 0; i < variables.Length; i++)
                    {
                        variables[i] = new Variables
                        {
                            RID = result.Id,
                            X = qr[0][i],
                            Y = qr[1][i]
                        };
                    }
                    context.Variables.InsertAllOnSubmit(variables);
                    context.SubmitChanges();
                }
            }
        }
    }
}
