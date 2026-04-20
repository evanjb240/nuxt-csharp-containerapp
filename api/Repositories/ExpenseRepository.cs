using SampleApp.Models;
using Npgsql;

namespace SampleApp.Repositories;

public interface IExpenseRepository
{
    Task<List<object>> GetExpensesByYearAsync(int year);
    Task<Guid> SaveExpenseAsync(object expense);
    Task DeleteExpenseAsync(Guid id);
    Task<List<object>> GetExpensesByFilterAsync(object filters);
}

public class ExpenseRepository(ICockroachDbConnectionProvider connectionProvider) : IExpenseRepository
{

    public async Task<List<object>> GetExpensesByYearAsync(int year)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> SaveExpenseAsync(object expense)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteExpenseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<object>> GetExpensesByFilterAsync(object filters)
    {
        throw new NotImplementedException();
    }
}
