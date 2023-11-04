using System.Runtime.Serialization;

namespace PersonalException
{
    [Serializable]
    public abstract class SerializableException : Exception
    {
        protected SerializableException()
        {
        }

        protected SerializableException(string message) : base(message)
        {
        }

        protected SerializableException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SerializableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);
        }
    }
}
