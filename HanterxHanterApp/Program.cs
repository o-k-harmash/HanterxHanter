using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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

app.UseStaticFiles(); // Это для раздачи статических файлов из папки wwwroot

// Настройка раздачи медиафайлов из произвольной папки (например, /media)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});

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

        // Добавление данных в Cities
        if (!ctx.Cities.Any())
        {
            ctx.Cities.AddRange(
                new City { CityId = "new_york", StateId = "california" },
                new City { CityId = "los_angeles", StateId = "california" }
            );

            ctx.SaveChanges();
        }

        // Добавление переводов для Cities
        if (!ctx.Translates.Any(x => x.Key == "new_york" || x.Key == "los_angeles"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "new_york", Value = "Нью-Йорк", LangCode = "ru", ToTable = "Cities" }, // перевод на русский
                new Translate { Key = "new_york", Value = "New York", LangCode = "en", ToTable = "Cities" }, // перевод на английский
                new Translate { Key = "new_york", Value = "Nueva York", LangCode = "es", ToTable = "Cities" },  // перевод на испанский
                new Translate { Key = "los_angeles", Value = "Лос-Анджелес", LangCode = "ru", ToTable = "Cities" }, // перевод на русский
                new Translate { Key = "los_angeles", Value = "Los Angeles", LangCode = "en", ToTable = "Cities" }, // перевод на английский
                new Translate { Key = "los_angeles", Value = "Los Ángeles", LangCode = "es", ToTable = "Cities" }  // перевод на испанский
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
                new Translate { Key = "book", Value = "Книга", LangCode = "ru", ToTable = "Interests" },
                new Translate { Key = "book", Value = "Book", LangCode = "en", ToTable = "Interests" },
                new Translate { Key = "sport", Value = "Спорт", LangCode = "ru", ToTable = "Interests" },
                new Translate { Key = "sport", Value = "Sport", LangCode = "en", ToTable = "Interests" },
                new Translate { Key = "study", Value = "Учёба", LangCode = "ru", ToTable = "Interests" },
                new Translate { Key = "study", Value = "Study", LangCode = "en", ToTable = "Interests" },
                new Translate { Key = "traveling", Value = "Путешествия", LangCode = "ru", ToTable = "Interests" },
                new Translate { Key = "traveling", Value = "Traveling", LangCode = "en", ToTable = "Interests" }
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
                new Translate { Key = "male", Value = "Мужской", LangCode = "ru", ToTable = "Genders" },
                new Translate { Key = "male", Value = "Male", LangCode = "en", ToTable = "Genders" },
                new Translate { Key = "female", Value = "Женский", LangCode = "ru", ToTable = "Genders" },
                new Translate { Key = "female", Value = "Female", LangCode = "en", ToTable = "Genders" }
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
                new Translate { Key = "russian", Value = "Русский", LangCode = "ru", ToTable = "Languages" },
                new Translate { Key = "russian", Value = "Russian", LangCode = "en", ToTable = "Languages" },
                new Translate { Key = "english", Value = "Английский", LangCode = "ru", ToTable = "Languages" },
                new Translate { Key = "english", Value = "English", LangCode = "en", ToTable = "Languages" }
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

        // Проверка на наличие данных в таблице RelationshipGoal
        if (!ctx.SexualOrientations.Any())
        {
            // Добавление записей в таблицу RelationshipGoal
            ctx.SexualOrientations.AddRange(
                new SexualOrientation { SexualOrientationId = "partnership" },
                new SexualOrientation { SexualOrientationId = "casual" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для RelationshipGoals
        if (!ctx.Translates.Any(t => t.Key == "partnership" || t.Key == "casual"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "partnership", Value = "Партнёрство", LangCode = "ru", ToTable = "RelationshipGoals" },
                new Translate { Key = "partnership", Value = "Partnership", LangCode = "en", ToTable = "RelationshipGoals" },
                new Translate { Key = "casual", Value = "Касуальный", LangCode = "ru", ToTable = "RelationshipGoals" },
                new Translate { Key = "casual", Value = "Casual", LangCode = "en", ToTable = "RelationshipGoals" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для SexualOrientations
        if (!ctx.Translates.Any(t => t.Key == "heterosexual" || t.Key == "homosexual"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "heterosexual", Value = "Гетеросексуальный", LangCode = "ru", ToTable = "SexualOrientations" },
                new Translate { Key = "heterosexual", Value = "Heterosexual", LangCode = "en", ToTable = "SexualOrientations" },
                new Translate { Key = "homosexual", Value = "Гомосексуальный", LangCode = "ru", ToTable = "SexualOrientations" },
                new Translate { Key = "homosexual", Value = "Homosexual", LangCode = "en", ToTable = "SexualOrientations" },
                new Translate { Key = "bisexual", Value = "Бисексуальный", LangCode = "ru", ToTable = "SexualOrientations" },
                new Translate { Key = "bisexual", Value = "Bisexual", LangCode = "en", ToTable = "SexualOrientations" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для Countries
        if (!ctx.Translates.Any(t => t.Key == "usa" || t.Key == "uk"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "usa", Value = "США", LangCode = "ru", ToTable = "Countries" },
                new Translate { Key = "usa", Value = "USA", LangCode = "en", ToTable = "Countries" },
                new Translate { Key = "uk", Value = "Великобритания", LangCode = "ru", ToTable = "Countries" },
                new Translate { Key = "uk", Value = "United Kingdom", LangCode = "en", ToTable = "Countries" }
            );
            ctx.SaveChanges();
        }

        // Добавление переводов для States
        if (!ctx.Translates.Any(t => t.Key == "arizona" || t.Key == "california"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "arizona", Value = "Аризона", LangCode = "ru", ToTable = "States" },
                new Translate { Key = "arizona", Value = "Arizona", LangCode = "en", ToTable = "States" },
                new Translate { Key = "california", Value = "Калифорния", LangCode = "ru", ToTable = "States" },
                new Translate { Key = "california", Value = "California", LangCode = "en", ToTable = "States" }
            );
            ctx.SaveChanges();
        }

        if (!ctx.Translates.Any(t => t.Key == "basic_information" || t.Key == "interests" || t.Key == "languages" || t.Key == "sexual_orientation" || t.Key == "relationship_goal"))
        {
            ctx.Translates.AddRange(
                new Translate { Key = "basic_information", Value = "Базовая информация", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "basic_information", Value = "Basic Information", LangCode = "en", ToTable = "View" },
                new Translate { Key = "interests", Value = "Interests", LangCode = "en", ToTable = "View" },
                new Translate { Key = "interests", Value = "Интересы", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "languages", Value = "Языки", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "languages", Value = "Languages", LangCode = "en", ToTable = "View" },
                new Translate { Key = "relationship_goal", Value = "Relationship Goal", LangCode = "en", ToTable = "View" },
                new Translate { Key = "sexual_orientation", Value = "Sexual Orientation", LangCode = "en", ToTable = "View" },
                new Translate { Key = "relationship_goal", Value = "Цель Отношений", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "sexual_orientation", Value = "Сексуальная Ориентация", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "no_more_profiles_for_you", Value = "Больше нет профилей для тебя", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "loading", Value = "Загрузка", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "something_wrong", Value = "Что-то пошло не так", LangCode = "ru", ToTable = "View" },
                new Translate { Key = "no_more_profiles_for_you", Value = "No more profiles for you", LangCode = "en", ToTable = "View" },
                new Translate { Key = "loading", Value = "Loading", LangCode = "en", ToTable = "View" },
                new Translate { Key = "something_wrong", Value = "Something wrong", LangCode = "en", ToTable = "View" }
            );
            ctx.SaveChanges();
        }
    }
}
