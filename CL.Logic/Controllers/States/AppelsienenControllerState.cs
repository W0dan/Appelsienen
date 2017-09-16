using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CL.Controllers
{
    class AppelsienenControllerState
    {
        public AppelsienenControllerState()
        {
            AppelsienenList = new List<int>();
        }

        public List<int> AppelsienenList { set; get; }

        public bool DrawGridLines { get; set; }
    }
}
