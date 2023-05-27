namespace Cdr.ErrorManagement
{
    public class CdrError
    {
        public string Code { get; }
        public string Description { get; }
        public CdrError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
