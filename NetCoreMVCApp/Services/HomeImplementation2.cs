using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVCApp.Services
{
    public class HomeImplementation2 : IHomeService
    {
        public string T1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetHomeData()
        {
            return "Data from Home2";
        }
    }
}
