using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Security.Entities
{
   public class UserInfoMob
    {
       public UserInfoMob()
       { 
       }
      
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
        public string Email { get; set; }
        
        public string EmailCurrent { get; set; }
        public bool UserExists { get; set; }
       
        public int CustomerID { get; set; }
        public int Status { get; set; }

        public UserInfoMob(bool _UserExists)
        {
            this.UserExists = _UserExists;
        }
    }
    
}