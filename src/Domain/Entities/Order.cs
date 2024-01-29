using Domain.Enums;
using Domain.Notifications;
using Domain.Utils.ErrorsMessages;
using Domain.ValuesObjects;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public Guid Uid { get; private set; }

        public Customer? Customer { get; private set; }

        public IEnumerable<Item> Items { get; private set; }

        public OrderStatus Status { get; private set; }

        private decimal value = 0;
        public decimal Value 
        {
            get
            {
                return GetTotal();
            }
        }

        public MoneyOrder? MoneyOrder { get; private set; }

        private Order()
        {
            Items = new List<Item>();
        }

        private Order(Customer? customer, List<Item> items)
        {
            Uid = Guid.NewGuid();
            Customer = customer;
            Items = items;
            Status = OrderStatus.Created;

            Validate();
        }

        protected override void Validate()
        {
            if (!Items.Any())
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.InvalidOrder });
            }

            if (Customer is not null && Customer.Errors.Any())
            {
                Customer.Errors.ToList().ForEach(e => Errors.Add(e));
            }

            if (!Enum.IsDefined(typeof(OrderStatus), Status))
            {
                Errors.Add(new Notification { Code = 40000, Title = ErrorMessage.BadRequest, Message = ErrorMessage.InvalidOrderStatus });
            }
        }

        public static Order Create(Customer? customer, List<Item> items) => new Order(customer, items);

        public void SetStatus(OrderStatus status) => Status = status;

        public void SetItems(List<Item> items) => Items = items;

        public void SetMoneyOrder(MoneyOrder moneyOrder) => MoneyOrder = moneyOrder;

        public void Update(Customer? customer, List<Item> items, OrderStatus status)
        {
            Customer = customer;
            Items = items;
            Status = status;

            Validate();
        }

        private decimal GetTotal()
        {
            if (value > 0 && !Items.Any()) return value;

            decimal result = 0;

            Items.ToList().ForEach(p =>
            {
                result = result + (p.Quantity * p.Product!.PriceComposition!.UnitPrice);
            });

            return result;
        }
    }
}
