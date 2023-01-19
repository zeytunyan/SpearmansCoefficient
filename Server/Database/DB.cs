using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class DB : IDisposable
    {
        IDbConnection connection;

        public DB()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(LocalDB)\MSSQLLocalDB";
            builder.AttachDBFilename = @"C:\Users\Георгий\source\repos\Spearmans_RHO\Server\SpearmanRealization.mdf";
            builder.IntegratedSecurity = true;
            connection = new SqlConnection(builder.ConnectionString);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        /// <summary>Inserts data to database.</summary>
        /// <param name="uname">  Username.</param>
        /// <param name="res"> Calculated SpearmanRealization's coefficient.</param>
        /// <param name="concl">Conclusion based on SpearmanRealization's coefficient.</param>
        /// <param name="qr">  Array with user-entered attributes .</param>
        public void Insert(string uname, double res, string concl, int[][] qr)
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
            connection.Close();
        }
    }
}
