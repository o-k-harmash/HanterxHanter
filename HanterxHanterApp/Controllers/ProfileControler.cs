using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProfileServices;
using ServiceLayer.ProfileServices.Concrete;

namespace HanterxHanterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;

        // Конструктор контроллера
        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Получение списка профилей с фильтрацией и пагинацией.
        /// </summary>
        /// <param name="options">Параметры фильтрации и пагинации (например, город, пол, возраст и т.д.).</param>
        /// <returns>Возвращает отфильтрованный список профилей с пагинацией.</returns>
        [HttpGet("filtered")]
        public async Task<ActionResult<ProfileListCombinedDto>> GetProfiles([FromQuery] FilterPageOptions options)
        {
            try
            {
                // Вызов сервиса для получения профилей с фильтрацией и пагинацией
                var result = await _profileService.FilterPageProfileListAsync(options);

                // Возвращаем результат в формате 200 OK
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Логируем ошибку или обрабатываем её (по необходимости)
                // Возвращаем ошибку с кодом 500, если произошла внутренняя ошибка
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Создает нового пользователя на основе данных из ProfileDto.
        /// </summary>
        /// <param name="newProfile">DTO объект с данными для нового пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateProfile([FromForm] ProfileDto newProfile)
        {
            if (newProfile == null)
            {
                return BadRequest("Invalid Profile data.");
            }

            try
            {
                var profile = await _profileService.CreateProfileAsync(newProfile);
                return CreatedAtAction(nameof(GetProfile), new { id = profile.UserId }, profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновляет существующего пользователя на основе данных из ProfileDto.
        /// </summary>
        /// <param name="newProfile">DTO объект с новыми данными для пользователя.</param>
        /// <returns>Обновленный пользователь.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileDto newProfile)
        {
            if (newProfile == null)
            {
                return BadRequest("Invalid Profile data.");
            }

            try
            {
                var Profile = await _profileService.UpdateProfileAsync(newProfile);
                return Ok(Profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить пользователя по идентификатору (для проверки).
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        [HttpGet("{id}")]
        public IActionResult GetProfile(Guid id)
        {
            return Ok();
        }
    }
}
