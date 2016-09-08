using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.FileManager
{
    public class Roles
    {
        public int ApplicationID { get; set; }
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public Roles() { }
    }
}