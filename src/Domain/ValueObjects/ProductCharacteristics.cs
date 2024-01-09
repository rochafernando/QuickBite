namespace Domain.ValueObjects
{
    public class ProductCharacteristics : BasicCharacteristics
    {
        private string _imagePath;
        protected ProductCharacteristics(string name, string description, string imagePath) : base(name, description)
        {
            _imagePath = imagePath;
        }

        public string ImagePath => _imagePath;

        public static ProductCharacteristics Create(string name, string description, string imagePath)
        {
            return new ProductCharacteristics(name, description, imagePath);
        }
    }
}
