using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

public class EfCoreContext : DbContext
{
    private IDbContextTransaction _currentTransaction;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options) { }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<RelationshipGoal> RelationshipGoals { get; set; }
    public DbSet<SexualOrientation> SexualOrientations { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Translate> Translates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) //#E
    {
        modelBuilder.Entity<ProfileInterest>()
            .HasKey(x => new { x.ProfileId, x.InterestId });

        modelBuilder.Entity<ProfileLanguage>()
            .HasKey(x => new { x.ProfileId, x.LanguageId });

        modelBuilder.Entity<Translate>()
            .HasKey(x => new { x.ToTable, x.Key, x.LangCode });

        modelBuilder.Entity<Profile>()
            .HasIndex(x => new { x.GenderId, x.CityId, x.Age });
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
            return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            //RollbackTransaction();
            throw;
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}