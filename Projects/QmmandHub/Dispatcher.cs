using System;
using System.Threading.Tasks;
using Frognar.QmmandHub.Commands;
using Frognar.QmmandHub.Queries;

namespace Frognar.QmmandHub;

public class Dispatcher(IServiceProvider serviceProvider) : IQueryDispatcher, ICommandDispatcher {
  readonly IServiceProvider serviceProvider = serviceProvider;

  async Task<TResult> IQueryDispatcher.ExecuteAsync<TQuery, TResult>(TQuery query) {
    IQueryHandler<TQuery, TResult>? handler = ResolveQueryHandler<TQuery, TResult>();
    if (handler is null) {
      throw new Exception($"Query handler for {typeof(TQuery).Name} not found");
    }

    return await handler.HandleAsync(query);
  }
  
  IQueryHandler<TQuery, TResult>? ResolveQueryHandler<TQuery, TResult>() where TQuery : IQuery<TResult> {
    return (IQueryHandler<TQuery, TResult>?)serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));
  }

  async Task<TResult> ICommandDispatcher.ExecuteAsync<TCommand, TResult>(TCommand command) {
    ICommandHandler<TCommand, TResult>? handler = ResolveCommandHandler<TCommand, TResult>();
    if (handler is null) {
      throw new Exception($"Command handler for {typeof(TCommand).Name} not found");
    }

    return await handler.HandleAsync(command);
  }
  
  ICommandHandler<TCommand, TResult>? ResolveCommandHandler<TCommand, TResult>() where TCommand : ICommand<TResult> {
    return (ICommandHandler<TCommand, TResult>?)serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));
  }
}