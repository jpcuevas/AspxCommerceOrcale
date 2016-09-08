using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Security.Helpers
{
    public enum UserCreationStatus
    {
        DUPLICATE_EMAIL = 3,
        DUPLICATE_USER = 6,
        SUCCESS = 1
    }
}