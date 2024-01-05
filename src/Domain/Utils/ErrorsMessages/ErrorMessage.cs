using System.Diagnostics.CodeAnalysis;

namespace Domain.Utils.ErrorsMessages
{
    [ExcludeFromCodeCoverage]
    public static class ErrorMessage
    {
        public static string BadRequest = "Bad Request";
        public static string NameIsRequired = "Name is required";
        public static string DocumentIsNotValid = "Document is not valid";
        public static string EmailIsRequired = "E-mail is required";
        public static string EmailIsNotValid = "E-mail is not valid";
    }
}
