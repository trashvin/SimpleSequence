using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace SimpleSequence.StorageProvider
{
    /// <summary>
    /// The storage class for MSSQL DB.
    /// </summary>
    public class MSSQLStorage : IIDStorage
    {
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True
        /// <summary>
        /// The connection string.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// The method that generates the sequence ID.
        /// </summary>
        /// <returns>An ID of type long.</returns>
        public long GetNextSequenceID()
        {
            long id = ReadData();
            UpdateSequence(id + 1);
            return id;
        }
        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="source">The connection string to the DB.</param>
        public MSSQLStorage(string source)
        {
            Source = source;
        }

        private long ReadData()
        {
            long id = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(Source))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 id FROM SIMPLEIDSEQUENCE"))
                    {
                        command.Connection = connection;
                        SqlDataReader reader = command.ExecuteReader();

                        string result = "0";
                        while (reader.Read())
                        {
                            result = reader[0].ToString();
                        }
                        Int64.TryParse(result, out id);
                    }
                }
                return id;
            }
            catch
            {
                return 0;
            }
        }
        private void UpdateSequence(long next)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Source))
                {
                    string query = "UPDATE SIMPLEIDSEQUENCE SET ID = @id";
                    using (SqlCommand command = new SqlCommand(query))
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = next;

                        command.ExecuteNonQuery();
                    } 
                }
            }
            catch(Exception ex)
            {

            }
        }

    }
}
