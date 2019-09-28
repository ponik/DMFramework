using System;
using System.Runtime.Serialization;

namespace DM.Kernel
{
    public class DMException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="DMException"/> object.
        /// </summary>
        public DMException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="DMException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public DMException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="DMException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public DMException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public DMException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}