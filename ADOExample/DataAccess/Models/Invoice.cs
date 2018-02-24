using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample.DataAccess.Models
{
    class Invoice
    {

            public int InvoiceId
            {
                get; set;
            }
            public int CustomerId
            {
                get; set;
            }
            public DateTime InvoiceDate
            {
                get; set;
            }
            public string BillingAddress
            {
                get; set;
            }
            public double Total
            {
                get; set;
            }

    }
}
