public class EntityWithTranslationsDto
{
    public string Key { get; set; }
    public IEnumerable<TranslationDto> Translations { get; set; }
}