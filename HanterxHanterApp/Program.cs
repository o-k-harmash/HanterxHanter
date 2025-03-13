using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using ServiceLayer.ProfileServices.Concrete;
using ServiceLayer.UserServices.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EfCoreContext>(builder =>
        builder.UseNpgsql(@"Host=localhost;Username=postgres;Password=postgres;Database=postgres")
            .UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            }))
            .EnableSensitiveDataLogging());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<DropdownService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var ctx = scope.ServiceProvider.GetService<EfCoreContext>();

        SeedDataWithTranslations(ctx);
    }

    app.MapOpenApi();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

void SeedDataWithTranslations(EfCoreContext ctx)
{
    if (ctx != null)
    {
        // Проверка на наличие данных в таблице Countries
        if (!ctx.Countries.Any())
        {
            // Добавление записей в таблицу Countries
            ctx.Countries.AddRange(
                new Country { CountryId = "usa" },
                new Country { CountryId = "uk" },
                new Country { CountryId = "germany" }
            );
            ctx.SaveChanges();
        }

        // Проверка на наличие данных в таблице States
        if (!ctx.States.Any())
        {
            // Добавление записей в таблицу States
            ctx.States.AddRange(
                new State { StateId = "arizona", CountryId = "usa" },
                new State { StateId = "california", CountryId = "usa" },
                new State { StateId = "virginia", CountryId = "usa" },
                new State { StateId = "berlin", CountryId = "germany" }
            );
            ctx.SaveChanges();
        }

        if (!ctx.Cities.Any())
        {
            ctx.Cities.AddRange(
                new City { CityId = "new_york", StateId = "california" },
                new City { CityId = "los_angeles", StateId = "california" }
            );

            ctx.SaveChanges();
        }

        if (!ctx.Translates.Any(x => x.Key == "new_york" || x.Key == "los_angeles"))
        {
            // Добавление переводов для города New York
            ctx.Translates.AddRange(
                new Translate { Key = "new_york", Value = "Нью-Йорк", LangCode = "ru" }, // перевод на русский
                new Translate { Key = "new_york", Value = "New York", LangCode = "en" }, // перевод на английский
                new Translate { Key = "new_york", Value = "Nueva York", LangCode = "es" },  // перевод на испанский
                new Translate { Key = "los_angeles", Value = "Лос-Анджелес", LangCode = "ru" }, // перевод на русский
                new Translate { Key = "los_angeles", Value = "Los Angeles", LangCode = "en" }, // перевод на английский
                new Translate { Key = "los_angeles", Value = "Los Ángeles", LangCode = "es" }  // перевод на испанский
            );
        }

        // Проверка на наличие данных в таблице Interest
        if (!ctx.Interests.Any())
        {
            // Добавление записей в таблицу Interest
            ctx.Interests.AddRange(
                new Interest { InterestId = "book" },
                new Interest { InterestId = "sport" },
                new Interest { InterestId = "study" },
                new Interest { InterestId = "traveling" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для Interests
        if (!ctx.Translates.Any(t => t.Key == "book" || t.Key == "sport" || t.Key == "study" || t.Key == "traveling"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "book", Value = "Книга", LangCode = "ru" },
                new Translate { Key = "book", Value = "Book", LangCode = "en" },
                new Translate { Key = "sport", Value = "Спорт", LangCode = "ru" },
                new Translate { Key = "sport", Value = "Sport", LangCode = "en" },
                new Translate { Key = "study", Value = "Учёба", LangCode = "ru" },
                new Translate { Key = "study", Value = "Study", LangCode = "en" },
                new Translate { Key = "traveling", Value = "Путешествия", LangCode = "ru" },
                new Translate { Key = "traveling", Value = "Traveling", LangCode = "en" }
            );
            ctx.SaveChanges();
        }

        // Проверка на наличие данных в таблице Gender
        if (!ctx.Genders.Any())
        {
            // Добавление записей в таблицу Gender
            ctx.Genders.AddRange(
                new Gender { GenderId = "male" },
                new Gender { GenderId = "female" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для Genders
        if (!ctx.Translates.Any(t => t.Key == "male" || t.Key == "female"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "male", Value = "Мужской", LangCode = "ru" },
                new Translate { Key = "male", Value = "Male", LangCode = "en" },
                new Translate { Key = "female", Value = "Женский", LangCode = "ru" },
                new Translate { Key = "female", Value = "Female", LangCode = "en" }
            );
            ctx.SaveChanges();
        }

        // Проверка на наличие данных в таблице Language
        if (!ctx.Languages.Any())
        {
            // Добавление записей в таблицу Language
            ctx.Languages.AddRange(
                new Language { LanguageId = "russian" },
                new Language { LanguageId = "english" },
                new Language { LanguageId = "german" },
                new Language { LanguageId = "french" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для Languages
        if (!ctx.Translates.Any(t => t.Key == "russian" || t.Key == "english"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "russian", Value = "Русский", LangCode = "ru" },
                new Translate { Key = "russian", Value = "Russian", LangCode = "en" },
                new Translate { Key = "english", Value = "Английский", LangCode = "ru" },
                new Translate { Key = "english", Value = "English", LangCode = "en" }
            );
            ctx.SaveChanges();
        }

        // Проверка на наличие данных в таблице RelationshipGoal
        if (!ctx.RelationshipGoals.Any())
        {
            // Добавление записей в таблицу RelationshipGoal
            ctx.RelationshipGoals.AddRange(
                new RelationshipGoal { RelationshipGoalId = "partnership" },
                new RelationshipGoal { RelationshipGoalId = "casual" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для RelationshipGoals
        if (!ctx.Translates.Any(t => t.Key == "partnership" || t.Key == "casual"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "partnership", Value = "Партнёрство", LangCode = "ru" },
                new Translate { Key = "partnership", Value = "Partnership", LangCode = "en" },
                new Translate { Key = "casual", Value = "Касуальный", LangCode = "ru" },
                new Translate { Key = "casual", Value = "Casual", LangCode = "en" }
            );
            ctx.SaveChanges();
        }

        // Проверка на наличие данных в таблице SexualOrientation
        if (!ctx.SexualOrientations.Any())
        {
            // Добавление записей в таблицу SexualOrientation
            ctx.SexualOrientations.AddRange(
                new SexualOrientation { SexualOrientationId = "heterosexual" },
                new SexualOrientation { SexualOrientationId = "homosexual" },
                new SexualOrientation { SexualOrientationId = "bisexual" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для SexualOrientations
        if (!ctx.Translates.Any(t => t.Key == "heterosexual" || t.Key == "homosexual"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "heterosexual", Value = "Гетеросексуальный", LangCode = "ru" },
                new Translate { Key = "heterosexual", Value = "Heterosexual", LangCode = "en" },
                new Translate { Key = "homosexual", Value = "Гомосексуальный", LangCode = "ru" },
                new Translate { Key = "homosexual", Value = "Homosexual", LangCode = "en" },
                new Translate { Key = "bisexual", Value = "Бисексуальный", LangCode = "ru" },
                new Translate { Key = "bisexual", Value = "Bisexual", LangCode = "en" }
            );
            ctx.SaveChanges();
        }


        // Добавление переводов для Countries
        if (!ctx.Translates.Any(t => t.Key == "usa" || t.Key == "uk"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "usa", Value = "США", LangCode = "ru" },
                new Translate { Key = "usa", Value = "USA", LangCode = "en" },
                new Translate { Key = "uk", Value = "Великобритания", LangCode = "ru" },
                new Translate { Key = "uk", Value = "United Kingdom", LangCode = "en" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для States
        if (!ctx.Translates.Any(t => t.Key == "arizona" || t.Key == "california"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "arizona", Value = "Аризона", LangCode = "ru" },
                new Translate { Key = "arizona", Value = "Arizona", LangCode = "en" },
                new Translate { Key = "california", Value = "Калифорния", LangCode = "ru" },
                new Translate { Key = "california", Value = "California", LangCode = "en" }
            );
            ctx.SaveChanges();
        }

        // Сохранение изменений в базе данных
        ctx.SaveChanges();
    }
}
