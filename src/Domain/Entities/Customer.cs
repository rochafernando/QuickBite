using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class Customer : Entity
    {
        public Guid Uid { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Document { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        private Customer()
        {
            
        }

        private Customer(string name, string document, string email)
        {
            Uid = Guid.NewGuid();
            Name = name;
            Document = document;
            Email = email;

            Validate();
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.NameIsRequired });

            }

            if (string.IsNullOrEmpty(Document) || (Regex.Replace(Document, @"[^0-9]", string.Empty).Length != 11))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.DocumentIsNotValid });
            }

            if (string.IsNullOrEmpty(Email))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.EmailIsRequired });
                return;
            }

            //if (!Regex.IsMatch(Email, @"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i"))
            //{
            //    Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.EmailIsNotValid });
            //}
        }

        public static Customer Create(string name, string document, string email) => new Customer(name, document, email);
    }
}
