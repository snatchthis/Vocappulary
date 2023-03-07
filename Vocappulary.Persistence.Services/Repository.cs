using SQLite;
using Vocappulary.Persistence.Data;

namespace Vocappulary.Persistence.Services;
public abstract class Repository<TEntity> where TEntity : IEntity, new()
{
    private readonly string _dbPath;

    private const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    protected SQLiteAsyncConnection Database { get; set; }

    public Repository(string dbPath)
    {
        _dbPath = dbPath;
    }

    protected async Task Init()
    {
        if (Database != null)
            return;

        Database = new SQLiteAsyncConnection(_dbPath, Flags);
        await Database.CreateTableAsync<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        await Init();
        return await Database.Table<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetSingleAsync(int id)
    {
        await Init();
        return await Database.Table<TEntity>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task<int> SaveAsync(TEntity entity)
    {
        await Init();
        if (entity.Id != 0)
            return await Database.UpdateAsync(entity);
        else
            return await Database.InsertAsync(entity);
    }

    public virtual async Task<int> DeleteAsync(TEntity entity)
    {
        await Init();
        return await Database.DeleteAsync(entity);
    }

    public virtual async Task<int> DeletaAllAsync()
    {
        await Init();
        return await Database.DeleteAllAsync<TEntity>();
    }
}

