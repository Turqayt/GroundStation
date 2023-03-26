using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqVkNrWE5Gc0BAXWFKblR8QWNTfV9gBShNYlxTR3ZcQFtjTntWd0RlWnxX;Mgo+DSMBPh8sVXJ1S0d+X1RPckBAWHxLflF1VWRTe156dlRWESFaRnZdQV1gS3hTcUBmWH5YdXRc;ORg4AjUWIQA/Gnt2VFhhQlJBfVtdX2ZWfFN0RnNedVpwfldHcC0sT3RfQF5jSnxVdkJmXH5fdnJTTw==;MTQ3Mjg2MkAzMjMxMmUzMTJlMzMzNUYrWEoweVBhMjN3TnBYOGsxTjYvN2o4SWZidDc1WnN3VllrL01SeVZUUHc9;MTQ3Mjg2M0AzMjMxMmUzMTJlMzMzNVBTQTZoT2Iycm5hanZxNmlzSFlRdkxlczVmVmtIYkJicW1DTURpaU0yVWM9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RGQmFMYVF2R2BJeFR0dF9EYUwgOX1dQl9gSX1RcEVkXHpfcXNRR2k=;MTQ3Mjg2NUAzMjMxMmUzMTJlMzMzNWpjYVlzUjRCRERFRi9TZjl4L0RRUERYUUZCOHUrODUrWDJOaXFJMEVWekE9;MTQ3Mjg2NkAzMjMxMmUzMTJlMzMzNWFoNURWeU1ndUxjcUhKSHluMU5ISDdob3ZlcVptZ1BRQ0ZDNFJ4Y3RtV289;Mgo+DSMBMAY9C3t2VFhhQlJBfVtdX2ZWfFN0RnNedVpwfldHcC0sT3RfQF5jSnxVdkJmXH5feHVVTw==;MTQ3Mjg2OEAzMjMxMmUzMTJlMzMzNVQ3eWYrR3ZZdEV4dlV3V1Z0T2NsaitQVjNxdXpDTEZ6V0QzcTJUN2dRcE09;MTQ3Mjg2OUAzMjMxMmUzMTJlMzMzNUU3WWx4NnBMSmNNNXlDM0ZIb0hGb2k2c0dhYzFhYmV3eUh3Rk16SEtyQXM9;MTQ3Mjg3MEAzMjMxMmUzMTJlMzMzNWpjYVlzUjRCRERFRi9TZjl4L0RRUERYUUZCOHUrODUrWDJOaXFJMEVWekE9");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
