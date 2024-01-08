using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class Exceptions
    {
        //[Serializable]
        public class DalExistsException : Exception
        {
            

            public DalExistsException(string? message) : base(message)
            {
            }

            public DalExistsException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

            protected DalExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
    [Serializable]
    public class DalNotExistsException : Exception
    {
      

        public DalNotExistsException(string? message) : base(message)
        {
        }

        public DalNotExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DalNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
