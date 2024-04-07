using System.Runtime.Serialization;
namespace BO;

//BO Exceptions:

[Serializable]
public class BlAlreadyExistsException : Exception
{




    public BlAlreadyExistsException(string? message) : base(message)
    {
    }


    public BlAlreadyExistsException(string? message, Exception innerException) : base(message, innerException)
    {
    }
}

    [Serializable]
    public class BlDoesNotExistException : Exception
    {
        
        public BlDoesNotExistException(string? message) : base(message)
        {
        }

        public BlDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BlDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
[Serializable]
public class BlIsScheduled : Exception
{

    public BlIsScheduled(string? message) : base(message)
    {
    }

    public BlIsScheduled(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BlIsScheduled(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
public class BlCannotBeDeletedException : Exception
{


    public BlCannotBeDeletedException(string? message) : base(message)
    {
    }

    public BlCannotBeDeletedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BlCannotBeDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
    [Serializable]
    public class BlInvalidDataException : Exception
    {
      

        public BlInvalidDataException(string? message) : base(message)
        {
        }

        public BlInvalidDataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BlInvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

[Serializable]
public class BlInvalidDateException : Exception
{

    public BlInvalidDateException(string? message) : base(message)
    {
    }

    public BlInvalidDateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BlInvalidDateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
    [Serializable]
    public class BlNotAllTasksAreScheduled : Exception
    {
       

        public BlNotAllTasksAreScheduled(string? message) : base(message)
        {
        }

        public BlNotAllTasksAreScheduled(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BlNotAllTasksAreScheduled(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

