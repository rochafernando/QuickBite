using Domain.Entities;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;

namespace Domain.ValuesObjects
{
    public class Item : ValueObject
    {
        public Product? Product { get; private set; }

        public int Quantity { get; private set; }

        private Item()
        {
        }

        private Item(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Product!;
            yield return Quantity;
        }

        protected override void Validate()
        {
            if (Product != null)
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ProductIsRequired });
            }

            if (Quantity <= 0)
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.ProductQuantityIsRequired });
            }
        }

        public static Item Create(Product product, int quantity) => new Item(product, quantity);
        
    }
}
