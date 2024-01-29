namespace Domain.Interfaces.CQS
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand where TResult : IResult
    {
        Task<TResult?> HandleAsync(TCommand command);
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
