using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArrowServer
{
    internal class LiteEmployee
    {
        public string lastName;
        public string firstName;
        public string middleName;
        public string ipAddress;


        public LiteEmployee()
        { }

        public LiteEmployee(string lastName, string firstName, string middleName, string ipAddress)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.middleName = middleName;
            this.ipAddress = ipAddress;
        }
    }
}
