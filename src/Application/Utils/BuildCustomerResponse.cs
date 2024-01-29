using Application.Responses.Customer;
using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.Utils
{
    [ExcludeFromCodeCoverage]
    public static class BuildCustomerResponse
    {
        public static CustomerResponse? Create(Customer? customer)
        {
            if (customer == null) return null;

            return new CustomerResponse 
            { 
                Uid = customer.Uid, 
                Name = customer.Name, 
                Document = customer.Document, 
                Email = customer.Email 
            };
        }
    }
}
