using System;
using System.Collections.Generic;
using System.Linq;
namespace Domain.ValueObjects
{
    public class BasicCharacteristics
    {
        private string _name;
        private string _description;

        public string Name => _name;

        public string Description => _description;

        protected BasicCharacteristics(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public static BasicCharacteristics Create(string name, string description)
        {
            return new BasicCharacteristics(name, description);
        }   
    }
}
