namespace Cdr.ErrorManagement
{
    public partial class ErrorManager
    {
        public class ErrorCodes
        {
            private const string PREFIX = "CDR_API_ERR_";
            public const string INTERNAL_SERVER_ERROR = PREFIX + "001";
            public const string INVALID_JSON_DATA = PREFIX + "002";
            public const string INVALID_DATE = PREFIX + "003";
            public const string INVALID_CSV_FILE = PREFIX + "004";
        }
    }
}
