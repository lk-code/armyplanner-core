using System;

namespace ArmyPlanner.Exceptions
{
    public class MissingGameException : Exception
    {
        public string MissingGameKey { get; set; }

        public MissingGameException(string missingGameKey) : this(string.Empty, missingGameKey)
        {

        }

        public MissingGameException(string message, string missingGameKey) : base(message)
        {
            this.MissingGameKey = missingGameKey;
        }
    }
}