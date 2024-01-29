using Domain.Notifications;
using Domain.Utils.ErrorsMessages;

namespace Domain.ValuesObjects
{
    public class ProductCharacteristics : ValueObject
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string PathImage { get; private set; } = string.Empty;

        private ProductCharacteristics()
        {
            
        }

        private ProductCharacteristics(string name, string description, string pathImage)
        {
            Name = name;
            Description = description;
            PathImage = pathImage;

            Validate();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
            yield return PathImage;
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ProductNameIsRequired });
            }

            if (string.IsNullOrEmpty(Description))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ProductDescriptionIsRequired });
            }

            //if (string.IsNullOrEmpty(PathImage))
            //{
            //    Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ProductPathImageIsRequired });
            //}
        }

        public static ProductCharacteristics Create(string name, string description, string pathImage)
            => new ProductCharacteristics(name, description, pathImage);
    }
}
