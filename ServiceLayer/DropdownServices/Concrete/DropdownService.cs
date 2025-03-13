using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class DropdownService
{
    private readonly EfCoreContext _ctx;

    public DropdownService(EfCoreContext ctx) => _ctx = ctx;

    public async Task<Dictionary<string, Dictionary<string, string>>> GetAllWithTranslationsAsync()
    {
        return await _ctx.Translates
            .GroupBy(t => t.LangCode) // Группируем по языку
            .ToDictionaryAsync(
                g => g.Key, // Ключ словаря - это LangCode
                g => g.ToDictionary(
                    t => t.Key, // Ключ словаря - это Key
                    t => t.Value // Значение словаря - это перевод
                )
            );
    }


    // Method to get Interests with translations
    public IEnumerable<EntityDtoTranslated> GetAllInterestsWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Interest, Translate>(i => i.InterestId)
            .ToList();
    }

    // Method to get Cities with translations
    public IEnumerable<EntityDtoTranslated> GetAllCitiesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<City, Translate>(c => c.CityId)
            .ToList();
    }

    // Method to get Countries with translations
    public IEnumerable<EntityDtoTranslated> GetAllCountriesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Country, Translate>(c => c.CountryId)
            .ToList();
    }

    // Method to get Genders with translations
    public IEnumerable<EntityDtoTranslated> GetAllGendersWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Gender, Translate>(g => g.GenderId)
            .ToList();
    }

    // Method to get RelationshipGoals with translations
    public IEnumerable<EntityDtoTranslated> GetAllRelationshipGoalsWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<RelationshipGoal, Translate>(r => r.RelationshipGoalId)
            .ToList();
    }

    // Method to get SexualOrientations with translations
    public IEnumerable<EntityDtoTranslated> GetAllSexualOrientationsWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<SexualOrientation, Translate>(r => r.SexualOrientationId)
            .ToList();
    }

    // Method to get States with translations
    public IEnumerable<EntityDtoTranslated> GetAllStatesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<State, Translate>(r => r.StateId)
            .ToList();
    }

    // Method to get Languages with translations
    public IEnumerable<EntityDtoTranslated> GetAllLanguagesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Language, Translate>(r => r.LanguageId)
            .ToList();
    }
}
