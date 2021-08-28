using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVCApp.Exceptions
{
    public class UsernameException: Exception
    {
        public override string Message => "Your name is weird";
    }
}
