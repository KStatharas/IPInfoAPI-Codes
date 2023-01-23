namespace IPInfoAPI_Codes.Exceptions
{
    public class InvalidTwoLetterCodeException : Exception
    {
        public InvalidTwoLetterCodeException(String code) : base($"Invalid Two Letter Code: {code}") { }
    }
}
