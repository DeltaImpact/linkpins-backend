using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BackSide2.BL.Exceptions
{
    [Serializable]
    public class ParsePageServiceException : ApplicationException
    {
        public ParsePageServiceException()
        {
        }

        public ParsePageServiceException(string message) : base(message)
        {
        }

        public ParsePageServiceException(string message, Exception ex) : base(message)
        {
            Ex = ex;
        }

        // Конструктор для обработки сериализации типа
        protected ParsePageServiceException(SerializationInfo info,
            StreamingContext contex)
            : base(info, contex)
        {
        }

        public Exception Ex { get; }
    }
}
