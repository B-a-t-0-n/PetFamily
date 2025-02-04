namespace PetFamily.Application.Abstraction
{
    public interface IQueryHandler<TResponce, in TQuery> where TQuery : IQuery
    {
        Task<TResponce> Handle(TQuery command, CancellationToken cancellation = default);
    }
}
