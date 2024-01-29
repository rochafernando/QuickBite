using Domain.Notifications;
using Domain.Utils.ErrorsMessages;

namespace Domain.ValuesObjects
{
    public class AuditDate : ValueObject
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private AuditDate()
        {
            
        }

        private AuditDate(DateTime createdAt)
        {
            CreatedAt = createdAt;

            Validate();
        }

        private AuditDate(DateTime createdAt, DateTime updatedAt)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;

            Validate();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CreatedAt;
            yield return UpdatedAt ?? DateTime.MinValue;
        }

        protected override void Validate()
        {
            if (CreatedAt == DateTime.MinValue)
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.CreatedAtIsRequired });
            }

            if (UpdatedAt is not null && UpdatedAt == DateTime.MinValue)
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.UpdatedAtIsRequired });
            }
        }

        public static AuditDate Create(DateTime createdAt)
            => new AuditDate(createdAt);

        public static AuditDate Create(DateTime createdAt, DateTime updatedAt) 
            => new AuditDate(createdAt, updatedAt);
    }
}
