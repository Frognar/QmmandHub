namespace QmmandHub.Queries;

public interface IQueryDispatcher {
  Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}