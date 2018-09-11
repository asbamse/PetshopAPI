using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.Core.Entity
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
