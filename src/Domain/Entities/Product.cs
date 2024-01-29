<<<<<<< HEAD
﻿namespace Domain.Entities
{
    public abstract class Product
    {


=======
﻿using Domain.ValuesObjects;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public Guid Uid { get; private set; }
        public ProductCategory? Category { get; private set; }
        public ProductCharacteristics? Characteristics { get; private set; }
        public bool Sellable { get; private set; }
        public bool Enable { get; private set; }
        public PriceComposition? PriceComposition { get; private set; }
        public AuditDate? AuditDate { get; private set; }

        private Product()
        {
        }

        private Product(ProductCategory category, ProductCharacteristics characteristics, bool sellable, bool enable, PriceComposition priceComposition)
        {
            Uid = Guid.NewGuid();
            Category = category;
            Characteristics = characteristics;
            Sellable = sellable;
            Enable = enable;
            PriceComposition = priceComposition;
            AuditDate = AuditDate.Create(DateTime.Now);

            Validate();
        }


        protected override void Validate()
        {
            if (Category is not null && Category.Errors.Any())
            {
                Errors = Category.Errors;
            }

            if (Characteristics is not null && Characteristics.Errors.Any())
            {
                Characteristics.Errors.ToList().ForEach(e => Errors.Add(e));
            }

            if (PriceComposition is not null && PriceComposition.Errors.Any())
            {
                PriceComposition.Errors.ToList().ForEach(e => Errors.Add(e));
            }

            if (AuditDate is not null && AuditDate.Errors.Any())
            {
                AuditDate.Errors.ToList().ForEach(e => Errors.Add(e));
            }
        }

        public static Product Create(ProductCategory category, ProductCharacteristics characteristics, bool sellable, bool enable, PriceComposition priceComposition)
            => new Product(category, characteristics, sellable, enable, priceComposition);

        public void Update(ProductCategory category, ProductCharacteristics characteristics, bool sellable, bool enable, PriceComposition priceComposition, AuditDate auditDate)
        {
            Category = category;
            Characteristics = characteristics;
            Sellable = sellable;
            Enable = enable;
            PriceComposition = priceComposition;
            AuditDate = AuditDate.Create(auditDate.CreatedAt, DateTime.Now);

            Validate();
        }

        public void SetCategory(ProductCategory? category) => Category = category;
        public void SetCharacteristics(ProductCharacteristics? characteristics) => Characteristics = characteristics;
        public void SetPriceComposition(PriceComposition? priceComposition) => PriceComposition = priceComposition;
        public void SetAuditDate(AuditDate? auditDate) => AuditDate = auditDate;
>>>>>>> template
    }
}
