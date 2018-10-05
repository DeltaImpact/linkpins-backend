using System;
using System.Runtime.Serialization;

namespace BackSide2.BL.Exceptions
{
    [Serializable]
    public class ProfileServiceException : ApplicationException
    {
        public ProfileServiceException()
        {
        }

        public ProfileServiceException(string message) : base(message)
        {
        }

        public ProfileServiceException(string message, Exception ex) : base(message)
        {
            Ex = ex;
        }

        // Конструктор для обработки сериализации типа
        protected ProfileServiceException(SerializationInfo info,
            StreamingContext contex)
            : base(info, contex)
        {
        }

        public Exception Ex { get; }
    }

}
