namespace Domain.Interfaces.CQS
{
    public abstract class Request : IRequest
    {
        public Guid CorrelationId { get; protected set; }

        protected Request()
        {
            CorrelationId = Guid.NewGuid();
        }
    }
}
