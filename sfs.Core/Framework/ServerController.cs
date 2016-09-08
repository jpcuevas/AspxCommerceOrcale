#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace SageFrame.Framework
{
    public class ServerController
    {
        public ServerController()
        {
        }

        public bool IsWebFarm
        {
            get
            {
                return false;
            }
        }
        public bool IsSagever
        {
            get
            {
                return false;
            }
        }

    }
}
