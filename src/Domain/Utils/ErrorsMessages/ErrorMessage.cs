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
        public static string CommandIsNotValid = "Command is not valid";
        public static string QueryIsNotValid = "Query is not valid";
        public static string CustomerNotFound = "Customer not found";
        public static string CategoryNameIsRequired = "Category name is required";
        public static string CategoryDescriptionIsRequired = "Category description is required";
        public static string CategoryNotFound = "Product category not found";
    }
}
