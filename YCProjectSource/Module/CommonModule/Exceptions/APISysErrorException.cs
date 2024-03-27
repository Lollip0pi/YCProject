namespace Module.CommonModule.Exceptions
{
    public class APISysErrorException : Exception
    {
        public APISysErrorException(string statusCode, string statusDesc = null)
        {
            StatusCode = statusCode;
            StatusDesc = statusDesc;
        }

        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
    }
}