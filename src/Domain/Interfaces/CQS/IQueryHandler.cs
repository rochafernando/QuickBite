namespace Domain.Interfaces.CQS
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery where TResult : IResult
    {
        Task<TResult?> HandleAsync(TQuery query);
    }

    public interface IQueryHandler<TResult> where TResult : IEnumerable<IResult>
    {
        Task<TResult?> HandleAsync();
    }
}
