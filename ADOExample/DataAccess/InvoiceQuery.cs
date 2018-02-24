using ADOExample.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample.DataAccess
{
    class InvoiceQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;
        public List<Invoice> GetInvoiceByTrackFirstLetter(string firstCharacter)
        {
            //create using statement starting with connecting to database with sql connection string, Trusted_Connection = True is logging into server as our Windows user
            //using statement will automatically dispose the connection at the end - cant put anything in a using statement that can't be disposed (anything that doesn't implement the idispose interface)

            using (var connection = new SqlConnection(_connectionString))
            {
                //open connection before ExecuteReader(when query returns something)returns a sql data reader)
                connection.Open();

                var cmd = connection.CreateCommand();

                //create a command with sql query
                cmd.CommandText = @"select i.*
                                    from invoice i
                                    join InvoiceLine il on il.InvoiceId = i.InvoiceId
                                    where exists (select TrackId from Track 
                                                  where Name like @FirstLetter + '%' and TrackId = il.TrackId)";

                //PREVENT EXTERNAL SQL INJECTION!:
                //NEVER EVER use string interp in a SQL statement!!! Instead use parameters using @ for variables int the SQL statement ex: "where exists (select TrackId from Track where Name like @FirstLetter + '%' and TrackId = x.TrackId)"
                var FirstLetterParam = new SqlParameter("@FirstLetter", System.Data.SqlDbType.VarChar);
                FirstLetterParam.Value = firstCharacter;
                cmd.Parameters.Add(FirstLetterParam);

                //ExecuteReader when query returns something (returns a sql data reader)
                //ExecuteScaler returns whatever's in the top right - returning just one thing
                //ExecuteNonQuery when doing an update or delete statment (times when you don't want results back
                var reader = cmd.ExecuteReader();

                var invoices = new List<Invoice>();

                //using a while loop, as long as there are rows the loop will continue:
                while (reader.Read())
                {
                    var invoice = new Invoice
                    {

                        //Takes in the column (0 would be first common)
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString()),
                        CustomerId = int.Parse(reader["CustomerId"].ToString()),
                        BillingAddress = reader["BillingAddress"].ToString(),
                        InvoiceDate = DateTime.Parse(reader["InvoiceDate"].ToString()),
                        Total = double.Parse(reader["Total"].ToString())
                    };

                    invoices.Add(invoice);

                }

                return invoices;

            }
        }
    }
}
