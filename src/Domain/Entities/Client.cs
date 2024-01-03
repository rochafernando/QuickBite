using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class Client : Entity
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        private Client()
        {
            
        }

        private Client(string name, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;

            Validate();
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.NameIsRequired });
                
            }

            if (string.IsNullOrEmpty(Email))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.EmailIsRequired });
                return;
            }

            if (!Regex.IsMatch(Email, @"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i"))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.EmailIsNotValid });
            }
        }

        public static Client Create(string name, string email) => new Client(name, email);
    }
}
