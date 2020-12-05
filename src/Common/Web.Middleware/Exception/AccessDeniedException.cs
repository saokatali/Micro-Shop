using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Web.Middleware
{
    public class AccessDeniedException:Exception
    {
        public AccessDeniedException() : base()
        {
        }

        public AccessDeniedException(string message) : base(message)
        {
        }

        public AccessDeniedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
