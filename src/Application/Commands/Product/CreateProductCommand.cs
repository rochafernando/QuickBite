namespace Application.Commands.Product
{
    public class CreateProductCommand : Command
    {
        public string CategoryUid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PathImage { get; set; } = string.Empty;
        public bool Sellable { get; set; }
        public bool Enable { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ComboPrice { get; set; }
    }
}
