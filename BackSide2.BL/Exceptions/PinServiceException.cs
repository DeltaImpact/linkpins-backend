using System;
using System.Runtime.Serialization;

namespace BackSide2.BL.Exceptions
{
    [Serializable]
    public class PinServiceException : ApplicationException
    {
        public PinServiceException()
        {
        }

        public PinServiceException(string message) : base(message)
        {
        }

        public PinServiceException(string message, Exception ex) : base(message)
        {
            Ex = ex;
        }

        // Конструктор для обработки сериализации типа
        protected PinServiceException(SerializationInfo info,
            StreamingContext contex)
            : base(info, contex)
        {
        }

        public Exception Ex { get; }
    }
}