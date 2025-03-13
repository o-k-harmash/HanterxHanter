using DataLayer;

namespace ServiceLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly FileContext FileContext;
        public readonly EfCoreContext Ctx;

        public UnitOfWork(FileContext fileContext, EfCoreContext ctx)
            => (FileContext, Ctx) = (fileContext, ctx);

        public void Rollback()
        {
            try
            {
                // Откатываем изменения в контексте файловой системы
                FileContext.Rollback();

                // Откатываем транзакцию в контексте базы данных
                Ctx.RollbackTransaction();
            }
            catch (Exception ex)
            {
                // Логируем ошибку отката
                Console.WriteLine("Something went wrong during rollback: {0}", ex.Message);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                // Начинаем транзакцию в контексте базы данных
                await Ctx.BeginTransactionAsync();

                // Сохраняем изменения в базе данных
                await Ctx.SaveChangesAsync();

                // Сохраняем изменения в файловом хранилище
                FileContext.SaveChanges();

                // Коммитим транзакцию базы данных, завершая ее
                await Ctx.CommitTransactionAsync(Ctx.GetCurrentTransaction());
            }
            catch (Exception ex)
            {
                // Логируем ошибку и откатываем все изменения
                Console.WriteLine("Something went wrong during SaveChangesAsync: {0}", ex.Message);

                Rollback();

                throw;  // Пробрасываем исключение дальше
            }
        }
    }
}