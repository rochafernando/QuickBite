using Domain.Notifications;
using Domain.Utils.ErrorsMessages;

namespace Domain.ValuesObjects
{
    public class PriceComposition : ValueObject
    {
        public decimal UnitPrice { get; private set; }
        public decimal ComboPrice { get; private set; }

        private PriceComposition()
        {
            
        }

        private PriceComposition(decimal unitPrice, decimal comboPrice)
        {
            UnitPrice = unitPrice;
            ComboPrice = comboPrice;

            Validate();
        }


        public static PriceComposition Create(decimal unitPrice, decimal comboPrice) 
            => new PriceComposition(unitPrice, comboPrice);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UnitPrice; 
            yield return ComboPrice;
        }

        protected override void Validate()
        {
            if (UnitPrice <= 0)
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.UnitPriceIsRequired });
            }

            if (ComboPrice <= 0)
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ComboPriceIsRequired });
            }
        }
    }
}
