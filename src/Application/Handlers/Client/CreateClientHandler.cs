using Application.Commands;
using Application.Responses;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Client
{
    public class CreateClientHandler : ICommandHandler<CreateClientCommand, ClientResponse>
    {
        private readonly ILogger<CreateClientHandler> _logger;
        private readonly NotificationContext _notificationContext;
        private readonly IClientRepository _clientRepository;

        public CreateClientHandler(
            ILogger<CreateClientHandler> logger,
            NotificationContext notificationContext,
            IClientRepository clientRepository)
        {
            _logger = logger;
            _notificationContext = notificationContext;
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse?> HandleAsync(CreateClientCommand command)
        {
            _logger.LogInformation(
                "Request Received", 
                new 
                { 
                    Command = command, 
                    ClassName = nameof(CreateClientHandler), 
                    MethodName = nameof(HandleAsync) 
                });

            var client = Domain.Entities.Client.Create(command.Name, command.Email);
            
            _notificationContext.AddNotification(client);

            if (_notificationContext.HasNotifications) return null;

            await _clientRepository.AddAsync(client);

            return new ClientResponse { Id = client.Id, Name = client.Name, Email = client.Email };
        }
    }
}
