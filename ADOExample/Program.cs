using ADOExample.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Active Data Objects
namespace ADOExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstLetter = Console.ReadLine();

            var invoiceQuery = new InvoiceQuery();
            var invoices = invoiceQuery.GetInvoiceByTrackFirstLetter(firstLetter);

            foreach (var invoice in invoices)
            {
                Console.WriteLine($"Invoice ID {invoice.InvoiceId} was shipped to {invoice.BillingAddress}.");
            }

            Console.ReadLine();
        }
    }
}
