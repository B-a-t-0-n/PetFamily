namespace PetFamily.Application.Abstraction
{
    public interface IQueryHandler<TResponce, in TQuery> where TQuery : IQuery
    {
        Task<TResponce> Handle(TQuery query, CancellationToken cancellation = default);
    }
}
