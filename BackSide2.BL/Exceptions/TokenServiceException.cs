using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BackSide2.BL.Exceptions
{
    [Serializable]
    public class TokenServiceException : ApplicationException
    {
        public TokenServiceException()
        {
        }

        public TokenServiceException(string message) : base(message)
        {
        }

        public TokenServiceException(string message, Exception ex) : base(message)
        {
            Ex = ex;
        }

        // Конструктор для обработки сериализации типа
        protected TokenServiceException(SerializationInfo info,
            StreamingContext contex)
            : base(info, contex)
        {
        }

        public Exception Ex { get; }
    }

}
