namespace Domain.Interfaces.CQS
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand where TResult : IResult
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
