namespace DataLayer
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
        public void Rollback();
    }
}