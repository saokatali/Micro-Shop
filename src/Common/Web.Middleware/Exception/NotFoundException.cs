using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Web.Middleware
{
    public class NotFoundException: Exception
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
