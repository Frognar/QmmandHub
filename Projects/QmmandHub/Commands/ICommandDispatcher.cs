namespace QmmandHub.Commands;

public interface ICommandDispatcher {
  Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
}