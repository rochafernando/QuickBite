using System.Diagnostics.CodeAnalysis;

namespace Domain.Utils.ErrorsMessages
{
    [ExcludeFromCodeCoverage]
    public static class ErrorMessage
    {
        public static readonly string BadRequest = "Bad Request";
        public static readonly string NameIsRequired = "Name is required";
        public static readonly string DocumentIsNotValid = "Document is not valid";
        public static readonly string EmailIsRequired = "E-mail is required";
        public static readonly string EmailIsNotValid = "E-mail is not valid";
        public static readonly string CommandIsNotValid = "Command is not valid";
        public static readonly string QueryIsNotValid = "Query is not valid";
        public static readonly string CustomerNotFound = "Customer not found";
        public static readonly string CategoryNameIsRequired = "Category name is required";
        public static readonly string CategoryDescriptionIsRequired = "Category description is required";
        public static readonly string CategoryNotFound = "Product category not found";
        public static readonly string UnitPriceIsRequired = "Unit Price is required";
        public static readonly string ComboPriceIsRequired = "Combo Price is required";
        public static readonly string CreatedAtIsRequired = "CreatedAt is required";
        public static readonly string UpdatedAtIsRequired = "UpdatedAt is required";
        public static readonly string ProductNameIsRequired = "Product name is required";
        public static readonly string ProductDescriptionIsRequired = "Product description is required";
        public static readonly string ProductPathImageIsRequired = "Product path image is required";
        public static readonly string ProductNotFound = "Product not found";
    }
}
