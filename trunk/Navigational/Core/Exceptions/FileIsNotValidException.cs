using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Exceptions
{
    class FileIsNotValidException : ApplicationException
    {
        public FileIsNotValidException()
            : base("File Is Not Valid")
        {
        }
    }
}
