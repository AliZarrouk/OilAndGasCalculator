using System;

namespace Core.DataAccess
{
    public class BaseError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Exception ErrorException { get; set; }
    }
}
