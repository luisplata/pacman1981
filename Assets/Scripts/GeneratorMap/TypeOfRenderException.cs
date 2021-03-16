using System;
using System.Runtime.Serialization;

[Serializable]
internal class TypeOfRenderException : Exception
{
    public TypeOfRenderException()
    {
    }

    public TypeOfRenderException(string message) : base(message)
    {
    }

    public TypeOfRenderException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected TypeOfRenderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}