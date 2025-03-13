using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class DropdownController : ControllerBase
{
    private readonly DropdownService _dropdownService;

    public DropdownController(DropdownService dropdownService)
    {
        _dropdownService = dropdownService;
    }

    // Get all interests with translations
    [HttpGet("interests/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllInterestsWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения интересов с переводами
            var result = _dropdownService.GetAllInterestsWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Get all cities with translations
    [HttpGet("cities/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllCitiesWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения городов с переводами
            var result = _dropdownService.GetAllCitiesWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Get all countries with translations
    [HttpGet("countries/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllCountriesWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения стран с переводами
            var result = _dropdownService.GetAllCountriesWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Get all genders with translations
    [HttpGet("genders/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllGendersWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения гендеров с переводами
            var result = _dropdownService.GetAllGendersWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Get all relationship goals with translations
    [HttpGet("relationshipGoals/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllRelationshipGoalsWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения целей отношений с переводами
            var result = _dropdownService.GetAllRelationshipGoalsWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Get all sexual orientations with translations
    [HttpGet("sexualOrientation/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllSexualOrientationsWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения сексуальных ориентаций с переводами
            var result = _dropdownService.GetAllSexualOrientationsWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Get all languages with translations
    [HttpGet("language/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllLanguagesWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения языков с переводами
            var result = _dropdownService.GetAllLanguagesWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Get all states with translations
    [HttpGet("states/all")]
    public ActionResult<IEnumerable<EntityWithTranslationsDto>> GetAllStatesWithTranslations()
    {
        try
        {
            // Вызов сервиса для получения состояний с переводами
            var result = _dropdownService.GetAllStatesWithTranslations();

            // Возвращаем результат в формате 200 OK
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку или обрабатываем её (по необходимости)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
