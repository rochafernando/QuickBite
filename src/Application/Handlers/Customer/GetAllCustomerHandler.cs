using Application.Responses.Customer;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Customer
{
    public class GetAllCustomerHandler : IQueryHandler<IEnumerable<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerHandler(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerResponse>?> HandleAsync()
        {
            var customerList = await _customerRepository.GetAllAsync();

            return customerList?.ToList().Select(c => new CustomerResponse
            {
                Uid = c.Uid, Name = c.Name, Document = c.Document, Email = c.Email
            }).ToList();
        }
    }
}
