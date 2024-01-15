
using System.Runtime.Serialization;

namespace DO;


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
    [Serializable]
    public class DalDeletionImpossible : Exception
    {

        public DalDeletionImpossible(string? message) : base(message)
        {
        }

        public DalDeletionImpossible(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    
        protected DalDeletionImpossible(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    [Serializable]
    public class LevelDoesntExist : Exception
    {
        

        public LevelDoesntExist(string? message) : base(message)
        {
        }

        public LevelDoesntExist(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LevelDoesntExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class DalXMLFileLoadCreateException : Exception
    {
       

        public DalXMLFileLoadCreateException(string? message) : base(message)
        {
        }

        public DalXMLFileLoadCreateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DalXMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

