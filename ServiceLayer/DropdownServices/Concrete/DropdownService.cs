using System.Collections.Generic;
using System.Linq;

public class DropdownService
{
    private readonly EfCoreContext _ctx;

    public DropdownService(EfCoreContext ctx) => _ctx = ctx;

    // Method to get Interests with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllInterestsWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Interest, Translate>(i => i.InterestId)
            .ToList();
    }

    // Method to get Cities with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllCitiesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<City, Translate>(c => c.CityId)
            .ToList();
    }

    // Method to get Countries with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllCountriesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Country, Translate>(c => c.CountryId)
            .ToList();
    }

    // Method to get Genders with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllGendersWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Gender, Translate>(g => g.GenderId)
            .ToList();
    }

    // Method to get RelationshipGoals with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllRelationshipGoalsWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<RelationshipGoal, Translate>(r => r.RelationshipGoalId)
            .ToList();
    }

    // Method to get SexualOrientations with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllSexualOrientationsWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<SexualOrientation, Translate>(r => r.SexualOrientationId)
            .ToList();
    }

    // Method to get States with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllStatesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<State, Translate>(r => r.StateId)
            .ToList();
    }

    // Method to get Languages with translations
    public IEnumerable<EntityWithTranslationsDto> GetAllLanguagesWithTranslations()
    {
        return _ctx
            .GetEntityWithTranslations<Language, Translate>(r => r.LanguageId)
            .ToList();
    }
}
