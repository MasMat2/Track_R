using System;

namespace TrackrAPI.Helpers
{
    public class UnathorizedException : CdisException
    {
        public UnathorizedException(string errorMessage): base(errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public new string ErrorMessage
        {
            get; set;
        }
        
    }
}
