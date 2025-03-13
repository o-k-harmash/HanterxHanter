namespace HxH.App.Models
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}