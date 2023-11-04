using System.Runtime.Serialization;

namespace PersonalException
{
    [Serializable]
    public class PasswordMismatchException : SerializableException
    {
        public PasswordMismatchException(string message = "Password and its confirmation do not match.")
            : base(message)
        {
        }

        protected PasswordMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }
    }
}
