namespace TrackrAPI.Helpers
{
    public class CdisException : Exception
    {
        public CdisException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage
        {
            get; set;
        }

        
    }
}
