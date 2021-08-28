using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVCApp.Exceptions
{
    public class EmailException : Exception
    {
        public override string Message => "Is it a real email?";
    }
}
