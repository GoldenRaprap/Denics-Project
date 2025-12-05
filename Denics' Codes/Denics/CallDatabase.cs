using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Denics
{
    internal class CallDatabase
    {
        String databaseStringName = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\RAPHAEL\\Desktop\\Denics Project\\Denics_ Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30";

        public CallDatabase() { }
        public String getDatabaseStringName()
        {
            return databaseStringName;
        }
    }
}
