public static class DbContextExtensions
{
    public static IQueryable<EntityDtoTranslated> GetEntityWithTranslations<T, J>(this EfCoreContext ctx, Func<T, string> id)
        where T : class
        where J : Translate
    {
        // 1. Загружаем все сущности T и вычисляем их ключи с использованием функции id(i)
        var entities = ctx.Set<T>()
            .ToList() // Загружаем все данные в память
            .Select(i => new
            {
                Entity = i,
                Key = id(i) // Применяем id к каждой сущности
            })
            .ToList(); // Сохраняем результат в память

        // 2. Загружаем все переводы в память
        var translations = ctx.Set<J>().ToList();

        // 3. Делаем Join и GroupBy в памяти
        var query = entities
            .Join(translations,
                i => i.Key,
                t => t.Key,
                (i, t) => new { Entity = i.Entity, Translation = t })
            .GroupBy(x => x.Translation.Key) // Группируем по вычисленному ключу
            .Select(g => new EntityDtoTranslated
            {
                Key = g.Key,
                Translates = g.Select(x => new TranslateDto
                {
                    LangCode = x.Translation.LangCode,
                    Value = x.Translation.Value
                }).ToArray()
            });

        return query.AsQueryable(); // Возвращаем как IQueryable для дальнейших операций
    }
}
