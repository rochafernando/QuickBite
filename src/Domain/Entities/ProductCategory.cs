using Domain.Notifications;
using Domain.Utils.ErrorsMessages;

namespace Domain.Entities
{
    public class ProductCategory : Entity
    {
        public Guid Uid { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        private ProductCategory()
        {
            
        }

        private ProductCategory(string name, string description)
        {
            Uid = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedAt = DateTime.Now;

            Validate();
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CategoryNameIsRequired });

            }

            if (string.IsNullOrEmpty(Description))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CategoryDescriptionIsRequired });

            }
        }

        public static ProductCategory Create(string name, string description) => new ProductCategory(name, description);

        public void Update(string name, string description)
        {
            Name = name;
            Description= description;

            Validate();
        }
    }
}
